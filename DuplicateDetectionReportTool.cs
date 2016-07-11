
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using InformationPanel = XrmToolBox.Extensibility.InformationPanel;

namespace CRMConsultants.DuplicateDetectionReport
{
    public partial class DuplicateDetectionReportTool : PluginControlBase
    {
        #region Constructor

        public DuplicateDetectionReportTool()
        {
            InitializeComponent();
            initialize();
        }

        #endregion Constructor

        #region # Private Methods #
        private void initialize()
        {
            ApplicationSetting.JobId = new List<Guid>();
        }
        private void LoadEntities()
        {
            cmbEntities.Enabled = true;
            cmbEntities.Items.Clear();
            cmbDuplicateDetectionJobs.DataSource = null;
            lvAttributes.Items.Clear();
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving Entities...",
                AsyncArgument = null,
                Work = (bw, e) =>
                {
                    var request = new RetrieveAllEntitiesRequest { EntityFilters = EntityFilters.Entity };
                    var response = (RetrieveAllEntitiesResponse)Service.Execute(request);

                    e.Result = response.EntityMetadata;     
                },

                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, "Error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var emds = (EntityMetadata[])e.Result;

                        foreach (var emd in emds)
                        {
                            if (emd.LogicalName == "account" || emd.LogicalName == "contact" || emd.LogicalName == "lead")
                                cmbEntities.Items.Add(new EntityData(emd.LogicalName, emd.DisplayName != null && emd.DisplayName.UserLocalizedLabel != null ? emd.DisplayName.UserLocalizedLabel.Label : "N/A", emd.PrimaryIdAttribute));
                        }
                    }
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        private bool IsJobCompleted(Guid jobId)
        {
            bool isCompleted = false;
            ColumnSet cs = new ColumnSet("statecode", "asyncoperationid");

            while (isCompleted == false)
            {
                var crmAsyncJobs = Service.RetrieveMultiple(new QueryExpression("asyncoperation")
                {
                    ColumnSet = cs,
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression("asyncoperationid",ConditionOperator.Equal, jobId)
                        }
                    }
                });
                foreach (var crmAsyncJob in crmAsyncJobs.Entities)
                {
                    if (crmAsyncJob != null && crmAsyncJob.Attributes.Contains("statecode") && crmAsyncJob.Attributes["statecode"] != null)
                    {
                        if (((OptionSetValue)crmAsyncJob.Attributes["statecode"]).Value == 3)
                        {
                            isCompleted = true;
                            return true;
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(2500);
                    }
                    //retryCount--;
                }
            }
            return isCompleted;
        }

        private void GetExistingJobs()
        {
            ColumnSet cs = new ColumnSet("name", "asyncoperationid");
            ApplicationSetting.ExistingJobIds = new Dictionary<Guid, string>();
            var crmAsyncJobs = Service.RetrieveMultiple(new QueryExpression("asyncoperation")
            {
                ColumnSet = cs,
                Criteria = new FilterExpression
                {
                    Conditions =
                        {
                            new ConditionExpression("operationtype",ConditionOperator.Equal, 8),
                            new ConditionExpression("statecode",ConditionOperator.Equal, 3),
                            new ConditionExpression("primaryentitytype",ConditionOperator.Equal, ApplicationSetting.SelectedEntity.LogicalName)
                        }
                }
            });
            this.Invoke(new Action(() =>
            {
                if (crmAsyncJobs != null && crmAsyncJobs.Entities.Count > 0)
                {
                    gbDuplicateDetectionJob.Visible = true;
                    //ApplicationSetting.ExistingJobIds.Add(Guid.em, "Please select");
                    foreach (var crmAsyncJob in crmAsyncJobs.Entities)
                    {
                        if (crmAsyncJob != null && crmAsyncJob.Attributes.Contains("name") && crmAsyncJob.Attributes["name"] != null)
                        {
                            ApplicationSetting.ExistingJobIds.Add(crmAsyncJob.Id, Convert.ToString(crmAsyncJob.Attributes["name"]));
                        }
                    }
                    cmbDuplicateDetectionJobs.DataSource = new BindingSource(ApplicationSetting.ExistingJobIds, null);
                    cmbDuplicateDetectionJobs.DisplayMember = "Value";
                    cmbDuplicateDetectionJobs.ValueMember = "Key";
                    cmbDuplicateDetectionJobs.Enabled = true;
                }
                else
                {
                    if (cmbDuplicateDetectionJobs.Items != null)
                        cmbDuplicateDetectionJobs.DataSource = null;
                    gbDuplicateDetectionJob.Visible = false;
                }
            }));
        }

        /// <summary>
        /// Get the Duplicate Records
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        private EntityCollection GetDuplicateRecords(Guid jobId)
        {
            QueryExpression queryExpresssion = new QueryExpression(ApplicationSetting.SelectedEntity.LogicalName)
            {
                ColumnSet = new ColumnSet(ApplicationSetting.AttributesSchemaList.ToArray()),
                Distinct = false,
                LinkEntities =
                                {
                                    new LinkEntity(ApplicationSetting.SelectedEntity.LogicalName, "duplicaterecord", ApplicationSetting.SelectedEntity.PrimaryIdAttribute, "baserecordid", JoinOperator.Inner)
                                    {
                                        LinkCriteria=
                                        {
                                            Conditions=
                                            {
                                                new ConditionExpression("asyncoperationid", ConditionOperator.Equal, jobId)
                                            }
                                        }
                                    },
                                     new LinkEntity(ApplicationSetting.SelectedEntity.LogicalName, "activitypointer", ApplicationSetting.SelectedEntity.PrimaryIdAttribute, "regardingobjectid", JoinOperator.LeftOuter)
                                    {
                                        Columns = new ColumnSet("activityid","activitytypecode"),
                                        EntityAlias="Activities",
                                    }
                             }

            };
            return Service.RetrieveMultiple(queryExpresssion);
        }

        /// <summary>
        /// Get the Duplicate Records
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        private EntityCollection GetRelatedDuplicateRecords(Guid [] entityId)
        {
            EntityCollection mainColl = new EntityCollection();
            int queryCount = 5000;

            // Initialize the page number.
            int pageNumber = 1;

            QueryExpression queryExpresssion = null;

            queryExpresssion = new QueryExpression("activitypointer")
            {
                ColumnSet = new ColumnSet("activityid", "activitytypecode", "regardingobjectid"),
                PageInfo = { PageNumber = pageNumber, PagingCookie = null, Count = queryCount },
                Distinct = false,
                Criteria =
             {
                   FilterOperator = LogicalOperator.And,
                      Conditions =
                         {
                            new ConditionExpression("regardingobjectid", ConditionOperator.In, entityId)
                         }
               },

            };

            while (true)
            {
                // Retrieve the page.
                EntityCollection results = Service.RetrieveMultiple(queryExpresssion);
                if (results.Entities != null)
                {
                    mainColl.Entities.AddRange(results.Entities);
                }

                // Check for more records, if it returns true.
                if (results.MoreRecords)
                {
                    // Increment the page number to retrieve the next page.
                    queryExpresssion.PageInfo.PageNumber++;

                    // Set the paging cookie to the paging cookie returned from current results.
                    queryExpresssion.PageInfo.PagingCookie = results.PagingCookie;
                }
                else
                {
                    // If no more records are in the result nodes, exit the loop.
                    break;
                }
            }
            return mainColl;
        }

        /// <summary>
        /// Get duplicate criterias
        /// </summary>
        /// <returns>EntityCollection</returns>
        private EntityCollection GetDuplicateCriterias()
        {
            RetrieveMultipleRequest rmRequest = null;
            RetrieveMultipleResponse rmResponse = null;

            rmRequest = new RetrieveMultipleRequest()
            {
                Query = new QueryExpression("duplicaterulecondition")
                {
                    ColumnSet = new ColumnSet(true),
                    //ColumnSet = new ColumnSet(true),
                    Distinct = true,
                    LinkEntities =
                                {
                                    new LinkEntity("duplicaterulecondition", "duplicaterule","regardingobjectid", "duplicateruleid", JoinOperator.Inner)
                                    {
                                        LinkCriteria=
                                        {
                                            Conditions=
                                            {
                                                new ConditionExpression("baseentityname", ConditionOperator.Equal, ApplicationSetting.SelectedEntity.LogicalName),
                                                new ConditionExpression("statuscode", ConditionOperator.Equal, 2)
                                            }

                                        }
                                    }
                               }
                }
            };

            rmResponse = (RetrieveMultipleResponse)Service.Execute(rmRequest);

            if (rmResponse.EntityCollection != null && rmResponse.EntityCollection.Entities.Count > 0)
            {
                return rmResponse.EntityCollection;
            }

            return null;
        }

        /// <summary>
        /// Get Attribute Value
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeType"></param>
        private string GetAttributeValue(Entity entity, string attributeName, object attibuteObj, string attributeType)
        {
            string attributeValue = "";

            switch (attributeType.ToUpper())
            {
                case "BOOLEAN":
                    {
                        attributeValue = Convert.ToString((bool)attibuteObj);
                    }
                    break;

                case "DATETIME":
                    {
                        attributeValue = Convert.ToString((DateTime)attibuteObj);
                    }
                    break;

                case "DECIMAL":
                    {
                        attributeValue = Convert.ToString((Decimal)attibuteObj);
                    }
                    break;

                case "DOUBLE":
                    {
                        attributeValue = Convert.ToString((Double)attibuteObj);
                    }
                    break;

                case "INT32":
                    {
                        attributeValue = Convert.ToString((int)attibuteObj);
                    }
                    break;

                case "MONEY":
                    {
                        attributeValue = Convert.ToString(((Money)attibuteObj).Value);
                    }
                    break;

                case "ENTITYREFERENCE":
                    {
                        attributeValue = Convert.ToString(((EntityReference)attibuteObj).Name);
                    }
                    break;

                case "MEMO":
                    {
                        attributeValue = Convert.ToString(attibuteObj);
                    }
                    break;

                case "OPTIONSETVALUE":
                    {
                        attributeValue = Convert.ToString(entity.FormattedValues[attributeName]);
                    }
                    break;

                case "PARTYLIST":
                    {
                        // Do nothing
                    }
                    break;

                case "STRING":
                    {
                        attributeValue = Convert.ToString(attibuteObj);
                    }
                    break;
                case "GUID":
                    {
                        attributeValue = Convert.ToString(attibuteObj);
                    }
                    break;
            }
            return attributeValue;
        }

        private void GenerateHeader(CreateExcelDoc excell_app)
        {
            int counter = 0;
            for (counter = 0; counter < ApplicationSetting.AttributesToDisplayName.Count(); counter++)
            {
                excell_app.createHeaders(1, counter + 1, ApplicationSetting.AttributesToDisplayName[counter], "A1", "A1", 0, "", true, 10, "n");
            }
            excell_app.createHeaders(1, counter + 1, "Activities Count", "A1", "A1", 0, "", true, 10, "n");
            excell_app.createHeaders(1, counter + 2, "Count of Fields entered", "A1", "A1", 0, "", true, 10, "n");
        }

        private void GetAttributesList()
        {
            ApplicationSetting.AttributesSchemaList = new List<string>();
            ApplicationSetting.AttributesDisplayList = new List<string>();
            ApplicationSetting.AttributesToDisplay = new List<string>();
            ApplicationSetting.AttributesToDisplayName = new List<string>();

            var selectedTags = this.lvAttributes.CheckedItems
                                 .Cast<ListViewItem>()
                               .Select(i => new { i.Tag, i.Text });

            foreach (var item in selectedTags)
            {
                ApplicationSetting.AttributesSchemaList.Add((string)item.Tag);
                ApplicationSetting.AttributesDisplayList.Add((string)item.Text);
                ApplicationSetting.AttributesToDisplay.Add((string)item.Tag);
                ApplicationSetting.AttributesToDisplayName.Add((string)item.Text);
            }

            var unselectedItems = this.lvAttributes.Items
                             .Cast<ListViewItem>().Where(i => i.Checked == false)
                           .Select(i => new { i.Tag, i.Text });

            foreach (var item in unselectedItems)
            {
                ApplicationSetting.AttributesSchemaList.Add((string)item.Tag);
                ApplicationSetting.AttributesDisplayList.Add((string)item.Text);
            }
        }

        /// <summary>
        /// Perform Merge operation
        /// </summary>
        private void MergeOperation()
        {
            #region # Varibale declaration #
            int progressCounter = 0;
            #endregion # Varibale declaration #

            if (ApplicationSetting.AttributesSchemaList == null || ApplicationSetting.AttributesSchemaList.Count <= 0)
            {
                MessageBox.Show(this, "Please select Attributes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            WorkAsync(new WorkAsyncInfo
            {
                Message = string.Concat("Duplicate ", ApplicationSetting.SelectedEntity.DisplayName, " sorting started......"),
                AsyncArgument = null,
                Work = (bw, evt) =>
                {
                    #region Duplicate Collection #
                    List<DuplicateRecords> duplicateRecords = new List<DuplicateRecords>();
                    foreach (DuplicateRecords record in ApplicationSetting.DuplicateRecords)
                    {
                        List<Entity> duplicatesCollection = new List<Entity>();

                        bw.ReportProgress(progressCounter * 100 / ApplicationSetting.DuplicateRecords.Count(), string.Concat("Generating duplicate report........"));
                        duplicatesCollection.AddRange(record.ChildEntity);

                            duplicatesCollection.Add(record.MasterEntity);

                        #region # Sort Master and Child #
                        if (duplicatesCollection != null && duplicatesCollection.Count() > 0)
                        {
                            IEnumerable<Entity> sortedRecord = null;
                            if (rbCreatedOn.Checked)
                            {
                                DateTime dateSortedFound = duplicatesCollection.Min(c => (DateTime)c.Attributes["createdon"]);
                                sortedRecord = duplicatesCollection.Where(c => (DateTime)c.Attributes["createdon"] == dateSortedFound);
                            }
                            else if (rbModifiedOn.Checked)
                            {
                                DateTime dateSortedFound = duplicatesCollection.Max(c => (DateTime)c.Attributes["modifiedon"]);
                                sortedRecord = duplicatesCollection.Where(c => (DateTime)c.Attributes["modifiedon"] == dateSortedFound);
                            }
                            else if (rbNoOfActivities.Checked)
                            {
                                int activitiesCount = duplicatesCollection.Max(c => (int)c.Attributes["ActivitiesCount"]);
                                sortedRecord = duplicatesCollection.Where(c => (int)c.Attributes["ActivitiesCount"] == activitiesCount);
                            }
                            else if (rbnNoOfCompletedFields.Checked)
                            {
                                int fieldsCount = duplicatesCollection.Max(c => (int)c.Attributes["CountOfCompletedFields"]);
                                sortedRecord = duplicatesCollection.Where(c => (int)c.Attributes["CountOfCompletedFields"] == fieldsCount);
                            }

                            // If only one record no need to query
                            if (sortedRecord != null && sortedRecord.Count() > 0)
                            {
                                Entity recordFound = sortedRecord.FirstOrDefault();
                                List<Entity> childRecords = duplicatesCollection.Where(c => c.Id != recordFound.Id).ToList();
                                List<Entity> childs = new List<Entity>();
                                if (recordFound != null && recordFound.Attributes.Contains("statecode") && ((OptionSetValue)recordFound.Attributes["statecode"]).Value == 0)
                                {
                                    if (childRecords != null && childRecords.Count() > 0)
                                    {
                                        List<Guid> ids = new List<Guid>();
                                        foreach (var childId in childRecords)
                                        {
                                            if (childId.Attributes.Contains("statecode") && ((OptionSetValue)childId.Attributes["statecode"]).Value == 0)
                                            {
                                                ids.Add(childId.Id);
                                                childs.Add(childId);
                                            }
                                        }
                                        duplicateRecords.Add(new DuplicateRecords { MasterId = recordFound.Id, ChildIds = ids, ChildEntity = childs, MasterEntity = recordFound });
                                    }
                                }
                            }
                        }
                        #endregion # Sort Master and Child #

                        progressCounter++;
                    }
                    #endregion Duplicate Collection #
                    progressCounter = 0;
                    bw.ReportProgress(1, "Duplicate merge started........");

                    #region # Merge #
                    for (int i = 0; i < duplicateRecords.Count(); i++)
                    {
                        bw.ReportProgress(progressCounter * 100 / ApplicationSetting.DuplicateRecords.Count(), string.Concat("Duplicate merge started........"));
                        DuplicateRecords duplicate = duplicateRecords[i];
                        MergeRecords(duplicate.MasterEntity, duplicate.ChildEntity);
                        progressCounter++;
                    }
                    this.Invoke(new Action(() => { btnMergeDuplicates.Enabled = false; }));

                    bw.ReportProgress(100, "Duplicate merge completed........");

                    this.Invoke(new Action(() => { MessageBox.Show(this, "Duplicate merge completed."); }));
                    #endregion # Merge #
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        this.Invoke(new Action(() => { MessageBox.Show(this, "An error occured " + evt.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }));
                    }
                },
                ProgressChanged = evt => { SetWorkingMessage(string.Format("{0}%\r\n{1}", evt.ProgressPercentage, evt.UserState)); }
            });
        }

        private void DeleteJob(Guid jobId, string entityName)
        {
            Service.Delete(entityName, jobId);
        }

        private void MergeRecords(Entity master, IEnumerable<Entity> subordinates)
        {
            // Create the target for the request.
            EntityReference target = new EntityReference();
            target.Id = master.Id;
            target.LogicalName = master.LogicalName;

            foreach (Entity e in subordinates)
            {
                if (master.Id != e.Id)
                {
                    Entity updateContent = new Entity(ApplicationSetting.SelectedEntity.LogicalName);
                    foreach (string attributeName in ApplicationSetting.AttributesSchemaList)
                    {
                        if (!master.Attributes.Contains(attributeName) && e.Attributes.Contains(attributeName))
                        {
                            string type = e.Attributes[attributeName].GetType().Name;

                            if (type.ToUpper() == "ENTITYREFERENCE")
                                updateContent.Attributes[attributeName] = new EntityReference(((EntityReference)e.Attributes[attributeName]).LogicalName, ((EntityReference)e.Attributes[attributeName]).Id);
                            else
                                updateContent.Attributes[attributeName] = e.Attributes[attributeName];
                        }
                    }

                    // Create the request.
                    MergeRequest merge = new MergeRequest();
                    merge.SubordinateId = e.Id;
                    merge.Target = target;
                    merge.PerformParentingChecks = false;
                    merge.UpdateContent = updateContent;
                    MergeResponse merged = (MergeResponse)Service.Execute(merge);
                }
            }
        }

        private bool DoesItContain(List<DuplicateRecords> duplicateRecords, Guid id )
        {
            bool isFound = false;
            if (duplicateRecords != null && duplicateRecords.Count > 0)
            {
                var masterId = duplicateRecords.Where(dup => dup.MasterId.Equals(id));

                if (masterId != null && masterId.Count() > 0)
                    isFound = true;
                else
                {
                    var childId = duplicateRecords.Where(dup => dup.ChildIds.Any(u => u == id));

                    if (childId != null && childId.Count() > 0)
                        isFound = true;
                }
            }
            return isFound;
        }

        private void UpdateJobStatus(Guid jobId)
        {
            try
            {
                Entity operation = new Entity("asyncoperation")
                {
                    Id = jobId
                };

                operation["statecode"] = new OptionSetValue(0);
                operation["statuscode"] = new OptionSetValue(0);

                Service.Update(operation);
            }
            catch (Exception ex) { }
        }

        #endregion # Private Methods #

        #region # Event Handlers #

        private void tsbLoadEntities_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadEntities);
        }

        public void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        private void cmbEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEntities.SelectedItem == null)
            {
                return;
            }

            this.Invoke(new Action(() =>
            {
                lvAttributes.Items.Clear();
                //cmbDuplicateDetectionJobs.Items.Clear();
                cmbDuplicateDetectionJobs.DataSource = null;
                tsbGenerate.Enabled = true;
                ApplicationSetting.AttributesSchemaWithTypes = new Dictionary<string, string>();
                btnBrowse.Enabled = true;
                txtFilePath.Text = "";
                ApplicationSetting.SelectedEntity = (EntityData)cmbEntities.SelectedItem;
            }));

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving attributes...",
                AsyncArgument = ApplicationSetting.SelectedEntity.LogicalName,
                Work = (bw, evt) =>
                {
                    var entityLogicalName = evt.Argument.ToString();

                    var request = new RetrieveEntityRequest { EntityFilters = EntityFilters.Attributes, LogicalName = entityLogicalName };
                    var response = (RetrieveEntityResponse)Service.Execute(request);

                    evt.Result = response.EntityMetadata;
                },
                PostWorkCallBack = evt =>
                {
                    foreach (var amd in ((EntityMetadata)evt.Result).Attributes)
                    {
                        if (amd.IsValidForRead == true)
                        {
                            var displayName = amd.DisplayName != null && amd.DisplayName.UserLocalizedLabel != null && amd.AttributeTypeName.Value != AttributeTypeDisplayName.ImageType
                                                  ? amd.DisplayName.UserLocalizedLabel.Label
                                                  : "";

                            if (displayName != string.Empty && !displayName.ToUpper().Contains("BASE"))
                            {
                                var item = new ListViewItem(displayName);
                                item.SubItems.Add(amd.LogicalName);
                                item.Tag = amd.LogicalName;

                                ApplicationSetting.AttributesSchemaWithTypes.Add(amd.LogicalName, amd.AttributeTypeName.Value);
                                lvAttributes.Items.Add(item);
                            }
                        }
                    }

                    // Bind the Jobs
                    GetExistingJobs();
                },
                ProgressChanged = evt => { SetWorkingMessage(string.Format("{0}%\r\n{1}", evt.ProgressPercentage, evt.UserState)); }
            });
        }

        private void tsbGenerate_Click(object sender, EventArgs e)
        {
            #region # Varibale declaration #
            EntityCollection entityColl = null;
            CreateExcelDoc excell_app = null;
            int progressCounter = 0;
            bool createJob = false;
            #endregion # Varibale declaration #

            if (txtFilePath.Text.Length == 0)
            {
                MessageBox.Show(this, "Please select file location.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.lvAttributes.CheckedItems == null || this.lvAttributes.CheckedItems.Count <= 0)
            {
                MessageBox.Show(this, "Please select Attributes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.cmbDuplicateDetectionJobs.SelectedItem == null && ApplicationSetting.ExistingJobIds != null && ApplicationSetting.ExistingJobIds.Count > 0)
            {
                // MessageBox.Show(this, "Please select existing Job to run.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult result = MessageBox.Show("Please select existing Job to run. If you want to run Job for all records click Yes, else click No button.", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    createJob = true;
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            else if (ApplicationSetting.ExistingJobIds.Count <= 0)
            {
                DialogResult result = MessageBox.Show("If you want to run Job for all records click Yes, else click No button.", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    createJob = true;
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }

            GetAttributesList();

            WorkAsync(new WorkAsyncInfo
            {
                Message = string.Concat("Duplicate ", ApplicationSetting.SelectedEntity.DisplayName, " detection started......"),
                AsyncArgument = null,
                Work = (bw, evt) =>
                {
                    #region # Bulk Detection #
                    Guid jobId = Guid.Empty;
                    bool isJobCompleted = false;
                    if (createJob == true)
                    {
                        BulkDetectDuplicatesRequest request = new BulkDetectDuplicatesRequest()
                        {
                            JobName = string.Format("{0} {1} {2}", "Detect duplicate", ApplicationSetting.SelectedEntity.DisplayName, DateTime.Now),
                            Query = new QueryExpression()
                            {
                                EntityName = ApplicationSetting.SelectedEntity.LogicalName,
                                ColumnSet = new ColumnSet(true)
                            },

                            RecurrencePattern = String.Empty,
                            RecurrenceStartTime = DateTime.Now,
                            ToRecipients = new Guid[0],
                            CCRecipients = new Guid[0]
                        };

                        BulkDetectDuplicatesResponse _response = (BulkDetectDuplicatesResponse)Service.Execute(request);
                        jobId = _response.JobId;
                        UpdateJobStatus(jobId);
                        isJobCompleted = IsJobCompleted(jobId);
                    }
                    else
                    {

                        this.Invoke(new Action(() => { jobId = new Guid(cmbDuplicateDetectionJobs.SelectedValue.ToString()); }));

                        isJobCompleted = true;
                    }

                    #endregion # Bulk Detection #

                    if (jobId != Guid.Empty && isJobCompleted)
                    {
                        ApplicationSetting.JobId.Add(jobId);

                        entityColl = GetDuplicateRecords(jobId);
                        ApplicationSetting.DuplicateCollection = new EntityCollection();
                        ApplicationSetting.DuplicateRecords = new List<DuplicateRecords>();

                        if (entityColl != null && entityColl.Entities.Count > 0)
                        {
                            this.Invoke(new Action(() =>
                            {
                                btnMergeDuplicates.Enabled = true;
                                rbCreatedOn.Enabled = true;
                                rbModifiedOn.Enabled = true;
                                rbNoOfActivities.Enabled = true;
                                rbnNoOfCompletedFields.Enabled = true;
                            }));

                            excell_app = new CreateExcelDoc(txtFilePath.Text.Trim());

                            GenerateHeader(excell_app);

                            bw.ReportProgress(100, string.Concat("Duplicate ", ApplicationSetting.SelectedEntity.DisplayName, " found (", entityColl.Entities.Count, ")"));

                            int row = 2;

                            bw.ReportProgress(0, "Retrieving duplicate records........");

                            IEnumerable<System.Linq.IGrouping<object, Entity>> entityCollGroupBy = entityColl.Entities.GroupBy(ent => ent.Attributes[ApplicationSetting.SelectedEntity.PrimaryIdAttribute]);
                            List<Guid> chidList = new List<Guid>();
                            foreach (IEnumerable<Entity> elements in entityCollGroupBy)
                            {
                                foreach (Entity entity in elements)
                                {
                                    bw.ReportProgress(progressCounter * 100 / entityColl.Entities.Count(), string.Concat("Generating duplicate report........"));
                                    if (entity != null)
                                    {
                                        if (!DoesItContain(ApplicationSetting.DuplicateRecords, entity.Id))
                                        {
                                            DuplicateRecords rec = new DuplicateRecords();
                                            rec.MasterId = entity.Id;
                                            if (elements != null && elements.Count() > 0)
                                            {
                                                var activityGroupBy = elements.Where(cnt => cnt.Attributes.Contains("Activities.activitytypecode") && cnt.Attributes["Activities.activitytypecode"] != null);
                                                entity.Attributes["ActivitiesCount"] = activityGroupBy != null ? activityGroupBy.Count() : 0;
                                                
                                            }
                                            else
                                            {
                                                entity.Attributes["ActivitiesCount"] = 0;
                                            }
                                            rec.MasterEntity = entity;

                                            Entity duplicateTemp = new Entity(ApplicationSetting.SelectedEntity.LogicalName);
                                            duplicateTemp.Id = entity.Id;
                                            var re = new RetrieveDuplicatesRequest
                                            {
                                                BusinessEntity = duplicateTemp,
                                                MatchingEntityName = duplicateTemp.LogicalName,
                                                PagingInfo = new PagingInfo() { PageNumber = 1, Count = 500 }
                                            };

                                            var response = (RetrieveDuplicatesResponse)Service.Execute(re);
                                            if (response != null && response.DuplicateCollection != null)
                                            {
                                                List<Entity> childRecords = new List<Entity>();
                                                rec.ChildIds = new List<Guid>();
                                                foreach (Entity duplicate in response.DuplicateCollection.Entities)
                                                {
                                                    if (!DoesItContain(ApplicationSetting.DuplicateRecords, duplicate.Id))
                                                    {
                                                        rec.ChildIds.Add(duplicate.Id);
                                                        chidList.Add(duplicate.Id);
                                                        childRecords.Add(duplicate);
                                                    }
                                                }                                              
                                                rec.ChildEntity = childRecords;
                                            }
                                            ApplicationSetting.DuplicateRecords.Add(rec);
                                        }
                                        progressCounter++;

                                        break;// Break every time...Row is repeating for every activity
                                    }
                                }
                            }

                            bw.ReportProgress(0, "Generating Duplicate report........");
                            progressCounter = 0;
                            List<DuplicateRecords> DuplicateRecords = ApplicationSetting.DuplicateRecords;
                            EntityCollection childs = new EntityCollection();
                            if (chidList != null && chidList.Count() > 0)
                            {
                                childs = GetRelatedDuplicateRecords(chidList.ToArray());
                            }

                            for (int indexofRecord = 0; indexofRecord < DuplicateRecords.Count(); indexofRecord++)
                            {
                                bw.ReportProgress(progressCounter * 100 / DuplicateRecords.Count(), string.Concat("Generating Duplicate report........"));
                                ApplicationSetting.DuplicateRecords[indexofRecord].MasterEntity = GetExcelPopulated(DuplicateRecords[indexofRecord].MasterEntity, 1, excell_app, ref row, null, Convert.ToInt32(DuplicateRecords[indexofRecord].MasterEntity.Attributes["ActivitiesCount"]), true);
                                for (int indexOfChild = 0; indexOfChild < DuplicateRecords[indexofRecord].ChildEntity.Count(); indexOfChild++)
                                {
                                    if (DuplicateRecords[indexofRecord].ChildEntity.ElementAt(indexOfChild) != null)
                                    {
                                        var activities = childs != null && childs.Entities.Count > 0 ? (childs.Entities.Where(i => ((EntityReference)i.Attributes["regardingobjectid"]).Id == DuplicateRecords[indexofRecord].ChildEntity.ElementAt(indexOfChild).Id).Count()) : 0;
                                        ApplicationSetting.DuplicateRecords[indexofRecord].ChildEntity[indexOfChild] = GetExcelPopulated(DuplicateRecords[indexofRecord].ChildEntity.ElementAt(indexOfChild), 1, excell_app, ref row, null, activities, false);
                                    }
                                }
                                progressCounter++;
                            }
                            bw.ReportProgress(100, "Duplicate detection completed........");

                            excell_app.worksheet.SaveAs(txtFilePath.Text);

                            this.Invoke(new Action(() => { MessageBox.Show(this, "Duplicate Detection Completed."); }));

                            this.Invoke(new Action(() =>
                            {
                                excell_app.app.DisplayAlerts = true;        
                                excell_app.app.ScreenUpdating = true;
                                excell_app.app.Visible = true;
                            }));

                            excell_app.ReleaseObject();
                            excell_app = null;

                            this.Invoke(new Action(() => { txtFilePath.Text = ""; }));
                        }
                        else
                        {
                            this.Invoke(new Action(() => { MessageBox.Show(this, "No Duplicates found."); }));
                        }

                        cmbDuplicateDetectionJobs.DataSource = null;
                        // Bind the Jobs
                        GetExistingJobs();
                    }
                },
                PostWorkCallBack = evt =>
                {
                    if (evt.Error != null)
                    {
                        this.Invoke(new Action(() => { MessageBox.Show(this, "An error occured " + evt.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }));
                    }
                },
                ProgressChanged = evt => { SetWorkingMessage(string.Format("{0}%\r\n{1}", evt.ProgressPercentage, evt.UserState)); }
            });
        }

        private Entity GetExcelPopulated(Entity entity, int column, CreateExcelDoc excell_app, ref int row, IEnumerable<Entity> elements, int countActivities, bool isParent)
        {
            int countOfCompletedFields = 0;
            foreach (string attributeName in ApplicationSetting.AttributesSchemaList)
            {
                if (entity.Attributes.Contains(attributeName) && entity.Attributes[attributeName] != null)
                {
                    countOfCompletedFields++;
                    if (ApplicationSetting.AttributesToDisplay.Contains(attributeName))
                    {
                        var attributeObj = entity.Attributes[attributeName];
                        string type = attributeObj.GetType().Name;
                        string attributeValue = GetAttributeValue(entity, attributeName, attributeObj, type);
                        excell_app.addData(row, column, attributeValue, "A" + row, "B" + row);
                    }
                }
                if (ApplicationSetting.AttributesToDisplay.Contains(attributeName))
                {
                    column++;
                }
            }
            #region # Activities Count#

            IEnumerable<Entity> activityGroupBy = null;

            if (elements != null && elements.Count() > 0)
                activityGroupBy = elements.Where(cnt => cnt.Attributes.Contains("Activities.activitytypecode") && cnt.Attributes["Activities.activitytypecode"] != null);

            int countOfActivities = activityGroupBy != null ? activityGroupBy.Count() : countActivities;

            if(!isParent)
            entity.Attributes["ActivitiesCount"] = countOfActivities;

            entity.Attributes["CountOfCompletedFields"] = countOfCompletedFields;
            excell_app.addData(row, column, Convert.ToString(countOfActivities), "A" + row, "B" + row);
            excell_app.addData(row, column + 1, Convert.ToString(countOfCompletedFields), "A" + row, "B" + row);
            row++;
            return entity;
            #endregion # Activities Count#
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Excel workbook|*.xlsx",
                Title = "Select a location for the file generated"
            };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                txtFilePath.Text = dialog.FileName;
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            SelUnselectAttributes(true);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            SelUnselectAttributes(false);
        }

        private void SelUnselectAttributes(bool check)
        {
            foreach (ListViewItem item in lvAttributes.Items)
            {
                item.Checked = check;
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void btnMergeDuplicates_Click(object sender, EventArgs e)
        {
            MergeOperation();
        }
        #endregion # Event Handlers #
    }
}
