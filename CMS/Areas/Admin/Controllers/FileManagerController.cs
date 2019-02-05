using System.Web.Mvc;
using System.Web.Mvc;
using GleamTech.FileUltimate;
using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web.UI.WebControls;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public partial class FileManagerController : Controller
    {
        public ActionResult Index()
        {
            FileManager fileManager;
            
            if(Request.QueryString["chooser"]!=null)
                fileManager = new FileManager
            {
                Width = Unit.Percentage(100), //Default unit is pixels. Percentage can be specified as Unit.Percentage(100)
                Height = Unit.Percentage(100),
                ID = "fileManager1",
                FullViewport = true,
                Resizable = true,
                DisplayLanguage = "en",
                ShowOnLoad = true,
                CollapseRibbon = true,
                ModalDialog = true,
                //ModalDialogTitle = "فایل را انتخاب کنید",
                ClientChosen = "fileManagerChosen",
                Chooser = true
            };
            else
                fileManager = new FileManager
            {
                Width = Unit.Percentage(100), //Default unit is pixels. Percentage can be specified as Unit.Percentage(100)
                Height = Unit.Percentage(100), 
            };


            //Create a root folder via assignment statements and add it to the control.
            var rootFolder = new FileManagerRootFolder();
            rootFolder.Name = "uploads";
             string location= "~/media/uploads/";
            rootFolder.Location = location;
            if (!string.IsNullOrEmpty(Request.QueryString["folder"]))
                location += Request.QueryString["folder"];
            
            

            fileManager.RootFolders.Add(rootFolder);

            /////////////////////////////////////////////////
            ////////////////////////////////////////////
            /////////////////////////////////////////
            //فولدر پیشفرض رو بتونه تعیین کنه
            //fileManager.InitialFolder;

            var accessControl = new FileManagerAccessControl();
            accessControl.Path = @"\";
            accessControl.AllowedPermissions = FileManagerPermissions.Full;
            rootFolder.AccessControls.Add(accessControl);
           
            var type = Request.QueryString["Type"];
            if (!string.IsNullOrEmpty(type))
                fileManager.InitialFolder = type + @":\";

            PopulateLanguageSelector();

            return View(fileManager);
        }
        public ActionResult Chooser()
        {
            var fileManager1 = new FileManager
            {
                ID = "fileManager1",
                Width = 800,
                Height = 400,
                Resizable = true,
                DisplayLanguage = "en",
                ShowOnLoad = false,
                CollapseRibbon = true,
                ModalDialog = true,
                ModalDialogTitle = "Choose a file",
                ClientChosen = "fileManagerChosen",
                Chooser = true
            };
            fileManager1.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager1.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            var fileManager2 = new FileManager
            {
                ID = "fileManager2",
                Width = 800,
                Height = 400,
                Resizable = true,
                DisplayLanguage = "en",
                ShowOnLoad = false,
                CollapseRibbon = true,
                ModalDialog = true,
                ModalDialogTitle = "Choose a folder",
                ClientChosen = "fileManagerChosen",
                Chooser = true,
                ChooserType = FileManagerChooserType.Folder
            };
            fileManager2.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager2.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            var fileManager3 = new FileManager
            {
                ID = "fileManager3",
                Width = 800,
                Height = 400,
                Resizable = true,
                DisplayLanguage = "en",
                ShowOnLoad = false,
                CollapseRibbon = true,
                ModalDialog = true,
                ModalDialogTitle = "Choose a file or a folder",
                ClientChosen = "fileManagerChosen",
                Chooser = true,
                ChooserType = FileManagerChooserType.FileOrFolder
            };
            fileManager3.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager3.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            var fileManager4 = new FileManager
            {
                ID = "fileManager4",
                Width = 800,
                Height = 400,
                Resizable = true,
                DisplayLanguage = "en",
                ShowOnLoad = false,
                CollapseRibbon = true,
                ModalDialog = true,
                ModalDialogTitle = "Choose files",
                ClientChosen = "fileManagerChosen",
                Chooser = true,
                ChooserMultipleSelection = true
            };
            fileManager4.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager4.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            var fileManager5 = new FileManager
            {
                ID = "fileManager5",
                Width = 800,
                Height = 400,
                Resizable = true,
                DisplayLanguage = "en",
                ShowOnLoad = false,
                CollapseRibbon = true,
                ModalDialog = true,
                ModalDialogTitle = "Choose folders",
                ClientChosen = "fileManagerChosen",
                Chooser = true,
                ChooserType = FileManagerChooserType.Folder,
                ChooserMultipleSelection = true
            };
            fileManager5.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager5.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            var fileManager6 = new FileManager
            {
                ID = "fileManager6",
                Width = 800,
                Height = 400,
                Resizable = true,
                DisplayLanguage = "en",
                ShowOnLoad = false,
                CollapseRibbon = true,
                ModalDialog = true,
                ModalDialogTitle = "Choose files or folders",
                ClientChosen = "fileManagerChosen",
                Chooser = true,
                ChooserType = FileManagerChooserType.FileOrFolder,
                ChooserMultipleSelection = true
            };
            fileManager6.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager6.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            var fileManager7 = new FileManager
            {
                ID = "fileManager7",
                Width = 800,
                Height = 400,
                Resizable = true,
                DisplayLanguage = "en",
                ShowOnLoad = false,
                ShowRibbon = false,
                ClientChosen = "fileManagerChosen",
                Chooser = true
            };
            fileManager7.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager7.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            return View(new[] { fileManager1, fileManager2, fileManager3, fileManager4, fileManager5, fileManager6, fileManager7 });
        }
        public ActionResult Dynamic()
        {
            var fileManager = new FileManager
            {
                Width = 800,
                Height = 600,
                Resizable = true
            };

            SetDynamicFolderAndPermissions(fileManager, Request["userSelector"] ?? "User1");

            PopulateUserSelector();

            return View(fileManager);
        }

        private void SetDynamicFolderAndPermissions(FileManager fileManager, string userName)
        {

            var rootFolder = new FileManagerRootFolder
            {
                Name = string.Format("Folder of {0}", userName),
                Location = string.Format("~/App_Data/RootFolder3/{0}", userName)
            };

            var accessControl = new FileManagerAccessControl { Path = @"\" };

            switch (userName)
            {
                case "User1":
                    accessControl.AllowedPermissions = FileManagerPermissions.Full;
                    break;
                case "User2":
                    accessControl.AllowedPermissions = FileManagerPermissions.ReadOnly | FileManagerPermissions.Upload;
                    break;
            }

            rootFolder.AccessControls.Add(accessControl);
            fileManager.RootFolders.Add(rootFolder);
        }

        private void PopulateUserSelector()
        {
            ViewBag.UserList = new SelectList(
                new[]
                {
                    new SelectListItem {Text = "User1 (Full permissions)", Value = "User1"},
                    new SelectListItem {Text = "User2 (ReadOnly plus Upload permissions)", Value = "User2"}
                },
                "Value",
                "Text",
                Request["userSelector"] ?? "User1"
            );
        }
        private const string EventLogSessionKey = "EventLog.CS";

        public ActionResult Events()
        {
            var fileManager = new FileManager
            {
                Width = 800,
                Height = 600,
                DisplayLanguage = "en"
            };
            fileManager.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            //Attached event handlers should be static methods because they are raised out of the context of the host page.
            //If instance methods are attached (eg. an instance method of Page class), this would cause memory leaks. 

            //Before Events which are fired before the action is started.
            //Use e.Cancel("message") within a before event handler for canceling the event and displaying a message to the user, 
            //When an event is canceled, the corresponding action will be canceled and the after event will not be fired.
            fileManager.Expanding += FileManagerExpanding;
            fileManager.Listing += FileManagerListing;
            fileManager.Creating += FileManagerCreating;
            fileManager.Deleting += FileManagerDeleting;
            fileManager.Renaming += FileManagerRenaming;
            fileManager.Copying += FileManagerCopying;
            fileManager.Moving += FileManagerMoving;
            fileManager.Compressing += FileManagerCompressing;
            fileManager.Extracting += FileManagerExtracting;
            fileManager.Uploading += FileManagerUploading;
            fileManager.Downloading += FileManagerDownloading;
            fileManager.Previewing += FileManagerPreviewing;

            //After Events which are fired after the action is completed.
            fileManager.Expanded += FileManagerExpanded;
            fileManager.Listed += FileManagerListed;
            fileManager.Created += FileManagerCreated;
            fileManager.Deleted += FileManagerDeleted;
            fileManager.Renamed += FileManagerRenamed;
            fileManager.Copied += FileManagerCopied;
            fileManager.Moved += FileManagerMoved;
            fileManager.Compressed += FileManagerCompressed;
            fileManager.Extracted += FileManagerExtracted;
            fileManager.Uploaded += FileManagerUploaded;
            fileManager.Downloaded += FileManagerDownloaded;
            fileManager.Previewed += FileManagerPreviewed;
            fileManager.Failed += FileManagerFailed;

            return View(fileManager);
        }

        public ActionResult GetLatestEvents()
        {
            var sb = new StringBuilder("<pre>");

            var eventLog = Session[EventLogSessionKey] as Stack<string>;
            if (eventLog == null || eventLog.Count == 0)
                sb.AppendLine("No events.");
            else
                while (eventLog.Count > 0)
                {
                    sb.AppendLine("Event " + eventLog.Count + " - " + DateTime.Now.ToString("T"));
                    sb.AppendLine(new String('-', 80));
                    sb.AppendLine(eventLog.Pop());
                }

            sb.AppendLine("</pre>");

            return Content(sb.ToString());
        }

        #region  Example event handlers for before events

        private static void FileManagerExpanding(object sender, FileManagerExpandingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Expanding"},
                    {"Path", e.Folder.FullPath},
                    {"Is Refresh", e.IsRefresh}
                });
        }

        private static void FileManagerListing(object sender, FileManagerListingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Listing"},
                    {"Path", e.Folder.FullPath},
                    {"Is Refresh", e.IsRefresh}
                });
        }

        private static void FileManagerCreating(object sender, FileManagerCreatingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Creating"},
                    {"Path", e.Folder.FullPath},
                    {"Creating Folder", e.ItemName}
                });
        }

        private static void FileManagerDeleting(object sender, FileManagerDeletingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Deleting"},
                    {"Path", e.Folder.FullPath},
                    {"Deleting Items", e.ItemNames}
                });
        }

        private static void FileManagerRenaming(object sender, FileManagerRenamingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Renaming"},
                    {"Path", e.Folder.FullPath},
                    {"Item Old Name", e.ItemName},
                    {"Item New Name", e.ItemNewName}
                });
        }

        private static void FileManagerCopying(object sender, FileManagerCopyingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Copying"},
                    {"From Path", e.Folder.FullPath},
                    {"Copying Items", e.ItemNames},
                    {"To Path", e.TargetFolder.FullPath},
                });
        }

        private static void FileManagerMoving(object sender, FileManagerMovingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Moving"},
                    {"From Path", e.Folder.FullPath},
                    {"Moving Items", e.ItemNames},
                    {"To Target Path", e.TargetFolder.FullPath},
                });
        }

        private static void FileManagerCompressing(object sender, FileManagerCompressingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Compressing"},
                    {"Path", e.Folder.FullPath},
                    {"Compressing Items", e.ItemNames},
                    {"Zip File", e.ZipFileName}
                });
        }

        private static void FileManagerExtracting(object sender, FileManagerExtractingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Extracting"},
                    {"Path", e.Folder.FullPath},
                    {"Extracting to Subfolder", e.ToSubfolder},
                    {"Archive File", e.ArchiveFileName}
                });
        }

        private static void FileManagerUploading(object sender, FileManagerUploadingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Uploading"},
                    {"Path", e.Folder.FullPath},
                    {"Upload Method", e.Method},
                    {"Uploading Files", e.Validations.Select(validation => new Dictionary<string, object>
                        {
                            {"Name", validation.Name},
                            {"Content Type", validation.ContentType},
                            {"Size", validation.Size.HasValue ? new ByteSizeValue(validation.Size.Value).ToFileSize().ToString() : "<unknown>"}
                        })
                    }
                });
        }

        private static void FileManagerDownloading(object sender, FileManagerDownloadingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Downloading"},
                    {"Path", e.Folder.FullPath},
                    {"Downloading Items", e.ItemNames},
                    {"Downloading File Name", e.DownloadFileName},
                    {"Opening in browser", e.OpenInBrowser}
                });
        }

        private static void FileManagerPreviewing(object sender, FileManagerPreviewingEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Previewing"},
                    {"Path", e.Folder.FullPath},
                    {"Previewing File", e.ItemName},
                    {"Previewer", e.PreviewerType}
                });
        }

        #endregion

        #region Example event handlers for after events

        private static void FileManagerExpanded(object sender, FileManagerExpandedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Expanded"},
                    {"Path", e.Folder.FullPath},
                    {"Is Refresh", e.IsRefresh}
                });
        }

        private static void FileManagerListed(object sender, FileManagerListedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Listed"},
                    {"Path", e.Folder.FullPath},
                    {"Is Refresh", e.IsRefresh}
                });
        }

        private static void FileManagerCreated(object sender, FileManagerCreatedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Created"},
                    {"Path", e.Folder.FullPath},
                    {"Created Folder", e.ItemName}
                });
        }

        private static void FileManagerDeleted(object sender, FileManagerDeletedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Deleted"},
                    {"Path", e.Folder.FullPath},
                    {"Deleted Items", e.ItemNames}
                });
        }

        private static void FileManagerRenamed(object sender, FileManagerRenamedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Renamed"},
                    {"Path", e.Folder.FullPath},
                    {"Item Old Name", e.ItemName},
                    {"Item New Name", e.ItemNewName}
                });
        }

        private static void FileManagerCopied(object sender, FileManagerCopiedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Copied"},
                    {"From Path", e.Folder.FullPath},
                    {"Source Items", e.ItemNames},
                    {"To Path", e.TargetFolder.FullPath},
                    {"Copied Items", e.TargetItemNames}
                });
        }

        private static void FileManagerMoved(object sender, FileManagerMovedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Moved"},
                    {"From Path", e.Folder.FullPath},
                    {"Moved Items", e.ItemNames},
                    {"To Path", e.TargetFolder.FullPath},
                });
        }

        private static void FileManagerCompressed(object sender, FileManagerCompressedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Compressed"},
                    {"Path", e.Folder.FullPath},
                    {"Compressed Items", e.ItemNames},
                    {"Zip File", e.ZipFileName}
                });
        }

        private static void FileManagerExtracted(object sender, FileManagerExtractedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Extracted"},
                    {"Path", e.Folder.FullPath},
                    {"Extracted to Subfolder", e.ToSubfolder},
                    {"Archive File", e.ArchiveFileName}
                });
        }

        private static void FileManagerUploaded(object sender, FileManagerUploadedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Uploaded"},
                    {"Path", e.Folder.FullPath},
                    {"Upload Method", e.Method},
                    {"Uploading Files", e.Items.Select(item => new Dictionary<string, object>
                        {
                            {"Name", (item.Status == UploadItemStatus.Completed) ? item.ReceivedName : item.ReceivingName},
                            {"Content Type", (item.Status == UploadItemStatus.Completed) ? item.ReceivedContentType : item.ReceivingContentType},
                            {"Size", new ByteSizeValue((item.Status == UploadItemStatus.Completed) ? item.ReceivedSize : item.ReceivingSize).ToFileSize()},
                            {"Status", item.Status},
                            {"Status Message", (!string.IsNullOrEmpty(item.StatusMessage)) ? Regex.Replace(item.StatusMessage, @"\n+", "\n\t\t") : "<none>"}
                        })
                    },
                    {"Total Size", new ByteSizeValue(e.TotalSize).ToFileSize()},
                    {"Elapsed Time", e.ElapsedTime},
                    {"Transfer Rate", e.TransferRate}
                });
        }

        private static void FileManagerDownloaded(object sender, FileManagerDownloadedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Downloaded"},
                    {"Path", e.Folder.FullPath},
                    {"Downloaded Items", e.ItemNames},
                    {"Downloaded File Name", e.DownloadFileName},
                    {"Opened in browser", e.OpenInBrowser},
                    {"Total Size", new ByteSizeValue(e.TotalSize).ToFileSize()},
                    {"Elapsed Time", e.ElapsedTime},
                    {"Transfer Rate", e.TransferRate}
                });
        }

        private static void FileManagerPreviewed(object sender, FileManagerPreviewedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Previewed"},
                    {"Path", e.Folder.FullPath},
                    {"Previewed File", e.ItemName},
                    {"Previewer", e.PreviewerType}
                });
        }

        private static void FileManagerFailed(object sender, FileManagerFailedEventArgs e)
        {
            SaveEventInfo(new Dictionary<string, object>
                {
                    {"Event Type", "Failed"},
                    {"Failed Action", e.FailedActionInfo.Name},
                    {"Parameters", e.FailedActionInfo.Parameters},
                    {"Error", e.Exception.ToString().Replace("\n", "\n\t")},
                });
        }

        #endregion

        private static void SaveEventInfo(Dictionary<string, object> eventInfo)
        {
            var resultText = new StringBuilder();
            foreach (var kvp in eventInfo)
            {
                resultText.Append(kvp.Key);
                resultText.Append(": \n");

                var enumerable = kvp.Value as IEnumerable;
                if (enumerable is string)
                    enumerable = null;
                if (enumerable != null)
                    foreach (var item in enumerable)
                    {
                        var subDictionary = item as Dictionary<string, object>;
                        if (subDictionary != null)
                            foreach (var subKvp in subDictionary)
                            {
                                resultText.Append("\t");
                                resultText.Append(subKvp.Key);
                                resultText.Append(": ");
                                resultText.Append(subKvp.Value);
                                resultText.Append("\n");
                            }
                        else
                        {
                            resultText.Append("\t");
                            resultText.Append(item);
                        }
                        resultText.Append("\n");
                    }
                else
                {
                    resultText.Append("\t");
                    resultText.Append(kvp.Value);
                    resultText.Append("\n");
                }
            }

            var context = System.Web.HttpContext.Current;
            var eventLog = context.Session[EventLogSessionKey] as Stack<string>;
            if (eventLog == null)
            {
                eventLog = new Stack<string>();
                context.Session[EventLogSessionKey] = eventLog;
            }
            if (eventLog.Count > 50)
                eventLog.Clear();

            eventLog.Push(resultText.ToString());
        }
        public ActionResult Layout()
        {
            var fileManager = new FileManager
            {
                Width = Unit.Percentage(100),
                DisplayLanguage = "en"
            };
            fileManager.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            return View(fileManager);
        }
        public ActionResult Overview()
        {
            var fileManager = new FileManager
            {
                Width = 800, //Default unit is pixels. Percentage can be specified as Unit.Percentage(100)
                Height = 600,
                Resizable = true
            };

            //Create a root folder via assignment statements and add it to the control.
            var rootFolder = new FileManagerRootFolder();
            rootFolder.Name = "1. Root Folder";

            /*
              For connecting as a specific user to a protected folder (UNC or local) use this format:
             
                Location="Path=\\server\share; User Name=USERNAME; Password=PASSWORD"
                Please see description of Location property on the right pane for more information.

              Information on FileManagerRootFolder.Location property as of v4.7:
         
                This property is now of type Location class instead of string. You can still assign
                a string to this property as it's automatically casted so this is not a breaking change. The advantage of this special
                Location class is that you can now set it directly to an instance of PhysicalLocation or AmazonS3Location (more will
                be available in the future) classes. For instance this line:
                
                rootFolder.Location = "Type=AmazonS3; Bucket Name=mybucket";
             
                is same as this line:
             
                rootFolder.Location = new AmazonS3Location { BucketName = "mybucket" };
             
                This means you don't need to bother with formatting location strings correctly (eg. guessing property names)
                Except in aspx markup, you will still need to use strings which look like connection strings if you need to set 
                advanced properties. Also note that this line:

                rootFolder.Location = "c:\some\folder";
             
                is same as this line:
             
                rootFolder.Location = "Type=Physical; Path=c:\some\folder";
             
                and also same as this line:
             
                rootFolder.Location = new PhysicalLocation { Path = "c:\some\folder" };
              
                So as in previous versions, setting location to a path string directly means it's a physical location by default.
            */
            rootFolder.Location = "~/App_Data/RootFolder1";

            fileManager.RootFolders.Add(rootFolder);
            var accessControl = new FileManagerAccessControl();
            accessControl.Path = @"\";
            accessControl.AllowedPermissions = FileManagerPermissions.Full;
            rootFolder.AccessControls.Add(accessControl);

            //Create another root folder. This time use object initializers (See CreateRootFolder2 method body).
            fileManager.RootFolders.Add(CreateRootFolder2());

            //Create the final root folder and add it to the control
            fileManager.RootFolders.Add(CreateRootFolder3());

            if (Request["languageSelector"] != null)
                fileManager.DisplayLanguage = Request["languageSelector"];

            PopulateLanguageSelector();

            return View(fileManager);
        }

        private FileManagerRootFolder CreateRootFolder2()
        {
            //Use object initializers instead of assignment statements for making the code more compact.

            var rootFolder = new FileManagerRootFolder
            {
                Name = "2. Feature Tests",
                Location = "~/App_Data/Feature Tests"
            };

            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.ReadOnly
            });

            //Access controls for subfolder "6. Permissions" and below
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions",
                AllowedPermissions = FileManagerPermissions.ListFiles
                                     | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\01. Full",
                AllowedPermissions = FileManagerPermissions.Full
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\02. Read",
                AllowedPermissions = FileManagerPermissions.ReadOnly
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\03. List subfolders",
                AllowedPermissions = FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\04. List files",
                AllowedPermissions = FileManagerPermissions.ListFiles
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\05. Create",
                AllowedPermissions = FileManagerPermissions.Create
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\06. Delete",
                AllowedPermissions = FileManagerPermissions.Delete
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\07. Rename",
                AllowedPermissions = FileManagerPermissions.Rename
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\08. Edit",
                AllowedPermissions = FileManagerPermissions.Edit
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\09. Upload",
                AllowedPermissions = FileManagerPermissions.Upload
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\10. Download",
                AllowedPermissions = FileManagerPermissions.Download
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\11. Compress",
                AllowedPermissions = FileManagerPermissions.Compress
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\12. Extract",
                AllowedPermissions = FileManagerPermissions.Extract
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\13. Cut",
                AllowedPermissions = FileManagerPermissions.Cut
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\14. Copy",
                AllowedPermissions = FileManagerPermissions.Copy
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\6. Permissions\15. Paste",
                AllowedPermissions = FileManagerPermissions.Paste
                | FileManagerPermissions.ListFiles
                | FileManagerPermissions.ListSubfolders
            });

            //Access controls for subfolder "7. File Type Restrictions" and below
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\7. File Type Restrictions\1. Only Image files (jpg, png, bmp, gif)",
                AllowedPermissions = FileManagerPermissions.Full,
                AllowedFileTypes = FileTypeSet.Parse("*.jpg|*.png|*.bmp|*.gif")
            });

            //Access controls for subfolder "8. Quota Restrictions" and below
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\8. Quota Restrictions\1. Quota (1 MB)",
                AllowedPermissions = FileManagerPermissions.Full,
                Quota = ByteSizeValue.Parse("1MB")
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\8. Quota Restrictions\2. Quota (15 MB)",
                AllowedPermissions = FileManagerPermissions.Full,
                Quota = ByteSizeValue.Parse("15MB")
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\8. Quota Restrictions\2. Quota (15 MB)\Quota (1 MB)",
                AllowedPermissions = FileManagerPermissions.Full,
                Quota = ByteSizeValue.Parse("1MB")
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\8. Quota Restrictions\2. Quota (15 MB)\Deep\Quota (1 MB)",
                AllowedPermissions = FileManagerPermissions.Full,
                Quota = ByteSizeValue.Parse("1MB")
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\8. Quota Restrictions\3. Quota (Unlimited)",
                AllowedPermissions = FileManagerPermissions.Full
            });
            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\8. Quota Restrictions\3. Quota (Unlimited)\Quota (1 MB)",
                AllowedPermissions = FileManagerPermissions.Full,
                Quota = ByteSizeValue.Parse("1MB")
            });

            return rootFolder;
        }

        private FileManagerRootFolder CreateRootFolder3()
        {
            //Create the final root folder and add it to the control
            var rootFolder = new FileManagerRootFolder
            {
                Name = "3. Another Root Folder",
                Location = "~/App_Data/RootFolder2"
            };

            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.ListSubfolders
                                     | FileManagerPermissions.ListFiles
                                     | FileManagerPermissions.Download
                                     | FileManagerPermissions.Upload,
                AllowedFileTypes = new FileTypeSet(new[] { "*.jpg", "*.gif" }), //or FileTypeSet.Parse("*.jpg|*.gif")
                Quota = new ByteSizeValue(2, ByteSizeUnit.MB) //or ByteSizeValue.Parse("2 MB")
            });

            rootFolder.AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\Subfolder1",
                AllowedPermissions = FileManagerPermissions.Full,
                DeniedPermissions = FileManagerPermissions.Download,
                DeniedFileTypes = new FileTypeSet(new[] { "*.exe" }) //or FileTypeSet.Parse("*.exe")
            });

            return rootFolder;
        }

        private void PopulateLanguageSelector()
        {
            ViewBag.LanguageList = new SelectList(
                FileUltimateConfiguration.AvailableDisplayCultures,
                "Name",
                "NativeName",
                Request["languageSelector"] ?? FileUltimateConfiguration.CurrentLanguage.ClosestCulture.Name
            );
        }
        public ActionResult Display()
        {
            var fileManager1 = new FileManager
            {
                ID = "fileManager1",
                Width = 800,
                Height = 600,
                DisplayLanguage = "en",
                ShowOnLoad = false
            };
            fileManager1.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager1.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            var fileManager2 = new FileManager
            {
                ID = "fileManager2",
                Width = 800,
                Height = 600,
                DisplayLanguage = "en",
                ShowOnLoad = false,
                FullViewport = true,
                ModalDialog = true,
                ModalDialogTitle = "FileManager as a modal dialog of viewport"
            };
            fileManager2.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager2.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            var fileManager3 = new FileManager
            {
                ID = "fileManager3",
                Width = 800,
                Height = 600,
                DisplayLanguage = "en",
                ShowOnLoad = false,
                ModalDialog = true,
                ModalDialogTitle = "FileManager as a modal dialog of parent element"
            };
            fileManager3.RootFolders.Add(new FileManagerRootFolder
            {
                Name = "Root Folder 1",
                Location = "~/App_Data/RootFolder1"
            });
            fileManager3.RootFolders[0].AccessControls.Add(new FileManagerAccessControl
            {
                Path = @"\",
                AllowedPermissions = FileManagerPermissions.Full
            });

            return View(new[] { fileManager1, fileManager2, fileManager3 });
        }
    }
}
