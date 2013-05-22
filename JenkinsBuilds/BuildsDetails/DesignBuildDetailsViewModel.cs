using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commons.Paths;
using JenkinsBuilds.Commands;
using JenkinsBuilds.Model;

namespace JenkinsBuilds.BuildsDetails
{
    public class DesignBuildDetailsViewModel : BuildDetailsViewModel
    {
        public DesignBuildDetailsViewModel()
        {
            this.Build = new Model.ExtendedBuildModel
            {
                FullDisplayName = "My Job Name #14",
                Cause = "Started by user Jan Kowalski",
                Status = Model.BuildStatus.Success,
                Duration = TimeSpan.FromMinutes(2) + TimeSpan.FromSeconds(15),
                Timestamp = new DateTime(2013, 04, 07, 12, 56, 12),

                ChangeSets = new[]
                {
                    new ChangeSetModel { Author = "Jack O'Neill",  Message = "Dialed the stargate" },
                    new ChangeSetModel { Author = "Sam Carter", Message = "Fixed it!" },
                },

                Artifacts = new Item[]
                {
                    new FileItem { Name = "File1.txt" },
                    new FolderItem 
                    {
                        Name = "Folder1",
                        Children = new List<Item>
                        {
                            new FolderItem { Name = "Folder2" },
                            new FolderItem { Name = "Folder3" },
                            new FileItem { Name = "File2.txt" },
                            new FileItem { Name = "File3.txt" },
                            new FileItem { Name = "File4.txt" },
                        }
                    }
                }
            };

            this.HasWarningsReport = true;

            this.Warnings = new Model.WarningsModel
            {
                FixedCount = 5,
                NewCount = 3,
                Count = 7,
                Delta = -2,
                Warnings = new List<Model.WarningModel>
                {
                    new Model.WarningModel { Key = 1, FileName="src/file1.cs", LineNumber = 20, Priority = "NORMAL", Message = @"The variable 'e' is declared but never used [D:\Jenkins\Clinic\workspace\src\Queries\Queries.csproj]" },
                    new Model.WarningModel { Key = 2, FileName="src/file2.cs", LineNumber = 20, Priority = "NORMAL", Message = @"The variable 'e' is declared but never used" },
                    new Model.WarningModel { Key = 3, FileName="src/file3.cs", LineNumber = 20, Priority = "NORMAL", Message = @"The variable 'e' is declared but never used [D:\Jenkins\Clinic\workspace\src\Queries\Queries.csproj]" },
                    new Model.WarningModel { Key = 4, FileName="src/file4.cs", LineNumber = 20, Priority = "NORMAL", Message = @"The variable 'e' is declared but never used" },
                    new Model.WarningModel { Key = 5, FileName="src/file5.cs", LineNumber = 20, Priority = "NORMAL", Message = @"The variable 'e' is declared but never used [D:\Jenkins\Clinic\workspace\src\Queries\Queries.csproj]" },
                    new Model.WarningModel { Key = 6, FileName="src/file5.cs", LineNumber = 20, Priority = "NORMAL", Message = @"The variable 'e' is declared but never used [D:\Jenkins\Clinic\workspace\src\Queries\Queries.csproj] The variable 'e' is declared but never used [D:\Jenkins\Clinic\workspace\src\Queries\Queries.csproj]" }
                }
            };

            this.TestResults = new Model.TestResultModel
            {
                Duration = TimeSpan.FromSeconds(1.7),
                FailCount = 1,
                PassCount = 39,
                SkipCount = 2,
                Suites = new List<Model.TestSuiteModel>
                {
                    new Model.TestSuiteModel
                    {
                        Name = "Suite1",
                        Duration = TimeSpan.FromSeconds(0.5),
                        Cases = new List<Model.TestCaseModel>
                        {
                            new Model.TestCaseModel {Name = "Test1", ClassName = "Suite1", Status = TestCaseStatus.Passed },
                            new Model.TestCaseModel {Name = "Test2", ClassName = "Suite1", Status = TestCaseStatus.Passed },
                            new Model.TestCaseModel {Name = "Test3", ClassName = "Suite1", Status = TestCaseStatus.Passed },
                        }
                    },
                    new Model.TestSuiteModel
                    {
                        Name = "Suite2",
                        Duration = TimeSpan.FromSeconds(1.2),
                        Cases = new List<Model.TestCaseModel>
                        {
                            new Model.TestCaseModel {Name = "Test1", ClassName = "Suite2", Status = TestCaseStatus.Passed },
                            new Model.TestCaseModel {Name = "Test2", ClassName = "Suite2", Status = TestCaseStatus.Failed },
                            new Model.TestCaseModel {Name = "Test3", ClassName = "Suite2", Status = TestCaseStatus.Skipped },
                        }
                    }
                }
            };

            this.HasTestResults = true;

            this.Job = new Model.JobModel
            {
                Name = "My Job name"
            };

            this.BuildLog = Task.FromResult(CreateLog());

            this.OpenBuildPageCommand = new DelegateCommand(o => { });
            this.RebuildCommand = new DelegateCommand(o => { });
            this.OpenConsoleLogCommand = new DelegateCommand(o => { });
            this.OpenWarningCommand = new DelegateCommand(o => { });
        }

        private string CreateLog()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < 200; i++)
            {
                sb.AppendLine(new string(i.ToString()[0], 80));
            }

            return sb.ToString();
        }
    }
}
