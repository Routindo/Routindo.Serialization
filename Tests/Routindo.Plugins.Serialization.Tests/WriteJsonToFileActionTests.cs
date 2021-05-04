using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Routindo.Contract;
using Routindo.Contract.Arguments;
using Routindo.Contract.Helpers;
using Routindo.Contract.Services;
using Routindo.Plugins.Serialization.Components.Actions.Json;
using Routindo.Plugins.Serialization.Tests.Mock;

namespace Routindo.Plugins.Serialization.Tests
{
    [TestClass]
    public class WriteJsonToFileActionTests
    { 
        private string _filePath;

        [TestInitialize]
        public void Init()
        {
            _filePath = Path.GetTempFileName();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if(!string.IsNullOrWhiteSpace(_filePath) && File.Exists(_filePath))
                File.Delete(_filePath);
        }

        #region no format 
        [TestMethod]
        [TestCategory("Integration Test")]
        public void WriteJsonToFileActionTest()
        {
            const bool format = false;
            WriteJsonToFileAction action = new WriteJsonToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteJsonToFileAction)),
                FilePath = _filePath, 
                WriteIndented = format
                // ,Append = false
            };
            var content = new FakeJsonObject()
            {
                IntegerProperty = 1,
                StringProperty = "I'm json object",
                ChildJsonObject = new FakeChildJsonObject()
                {
                    ChildIntegerProperty = 2,
                    ChildStringProperty = "I'm child json object"
                }
            }; 

            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteJsonToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            Assert.AreEqual(content.ToJsonString(format), fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNoNewLineJsonToFileActionTest()
        {
            const bool format = false;
            WriteJsonToFileAction action = new WriteJsonToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteJsonToFileAction)),
                FilePath = _filePath,
                Append = true,
                WriteIndented = format
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = new FakeJsonObject()
            {
                IntegerProperty = 1,
                StringProperty = "I'm json object",
                ChildJsonObject = new FakeChildJsonObject()
                {
                    ChildIntegerProperty = 2,
                    ChildStringProperty = "I'm child json object"
                }
            };
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteJsonToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(initialContent);
            stringBuilder.Append(content.ToJsonString(format));
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNewLineAfterJsonToFileActionTest()
        {
            const bool format = false;
            WriteJsonToFileAction action = new WriteJsonToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteJsonToFileAction)),
                FilePath = _filePath,
                Append = true, 
                NewLineBeforeAppend = false,
                NewLineAfterAppend = true,
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = new FakeJsonObject()
            {
                IntegerProperty = 1,
                StringProperty = "I'm json object",
                ChildJsonObject = new FakeChildJsonObject()
                {
                    ChildIntegerProperty = 2,
                    ChildStringProperty = "I'm child json object"
                }
            };
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteJsonToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(initialContent);
            stringBuilder.AppendLine(content.ToJsonString(format));
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNewLineBeforeJsonToFileActionTest()
        {
            const bool format = false;
            WriteJsonToFileAction action = new WriteJsonToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteJsonToFileAction)),
                FilePath = _filePath,
                Append = true,
                NewLineBeforeAppend = true,
                NewLineAfterAppend = false,
                WriteIndented = format
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = new FakeJsonObject()
            {
                IntegerProperty = 1,
                StringProperty = "I'm json object",
                ChildJsonObject = new FakeChildJsonObject()
                {
                    ChildIntegerProperty = 2,
                    ChildStringProperty = "I'm child json object"
                }
            };
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteJsonToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(initialContent);
            stringBuilder.Append(content.ToJsonString(format));
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNewLineBeforeAfterJsonToFileActionTest()
        {
            const bool format = false;
            WriteJsonToFileAction action = new WriteJsonToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteJsonToFileAction)),
                FilePath = _filePath,
                Append = true,
                NewLineBeforeAppend = true,
                NewLineAfterAppend = true,
                WriteIndented = format
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent); 
            var content = new FakeJsonObject()
            {
                IntegerProperty = 1,
                StringProperty = "I'm json object",
                ChildJsonObject = new FakeChildJsonObject()
                {
                    ChildIntegerProperty = 2,
                    ChildStringProperty = "I'm child json object"
                }
            };
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteJsonToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(initialContent);
            stringBuilder.AppendLine(content.ToJsonString(format));
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }
        #endregion

        #region format 

        [TestMethod]
        [TestCategory("Integration Test")]
        public void WriteJsonFormatToFileActionTest()
        {
            const bool format = true;
            WriteJsonToFileAction action = new WriteJsonToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteJsonToFileAction)),
                FilePath = _filePath,
                WriteIndented = format
                // ,Append = false
            };
            var content = new FakeJsonObject()
            {
                IntegerProperty = 1,
                StringProperty = "I'm json object",
                ChildJsonObject = new FakeChildJsonObject()
                {
                    ChildIntegerProperty = 2,
                    ChildStringProperty = "I'm child json object"
                }
            };

            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteJsonToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            var expectedContent = content.ToJsonString(format);
            Assert.AreEqual(expectedContent, fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNoNewLineJsonFormatToFileActionTest()
        {
            const bool format = true;
            WriteJsonToFileAction action = new WriteJsonToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteJsonToFileAction)),
                FilePath = _filePath,
                Append = true,
                WriteIndented = format
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = new FakeJsonObject()
            {
                IntegerProperty = 1,
                StringProperty = "I'm json object",
                ChildJsonObject = new FakeChildJsonObject()
                {
                    ChildIntegerProperty = 2,
                    ChildStringProperty = "I'm child json object"
                }
            };
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteJsonToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(initialContent);
            stringBuilder.Append(content.ToJsonString(format));
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNewLineAfterJsonFormatToFileActionTest()
        {
            const bool format = true;
            WriteJsonToFileAction action = new WriteJsonToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteJsonToFileAction)),
                FilePath = _filePath,
                Append = true,
                NewLineBeforeAppend = false,
                NewLineAfterAppend = true, WriteIndented = format
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = new FakeJsonObject()
            {
                IntegerProperty = 1,
                StringProperty = "I'm json object",
                ChildJsonObject = new FakeChildJsonObject()
                {
                    ChildIntegerProperty = 2,
                    ChildStringProperty = "I'm child json object"
                }
            };
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteJsonToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(initialContent);
            stringBuilder.AppendLine(content.ToJsonString(format));
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNewLineBeforeJsonFormatToFileActionTest()
        {
            const bool format = true;
            WriteJsonToFileAction action = new WriteJsonToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteJsonToFileAction)),
                FilePath = _filePath,
                Append = true,
                NewLineBeforeAppend = true,
                NewLineAfterAppend = false,
                WriteIndented = format
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = new FakeJsonObject()
            {
                IntegerProperty = 1,
                StringProperty = "I'm json object",
                ChildJsonObject = new FakeChildJsonObject()
                {
                    ChildIntegerProperty = 2,
                    ChildStringProperty = "I'm child json object"
                }
            };
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteJsonToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(initialContent);
            stringBuilder.Append(content.ToJsonString(format));
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNewLineBeforeAfterJsonFormatToFileActionTest()
        {
            const bool format = true;
            WriteJsonToFileAction action = new WriteJsonToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteJsonToFileAction)),
                FilePath = _filePath,
                Append = true,
                NewLineBeforeAppend = true,
                NewLineAfterAppend = true,
                WriteIndented = format
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = new FakeJsonObject()
            {
                IntegerProperty = 1,
                StringProperty = "I'm json object",
                ChildJsonObject = new FakeChildJsonObject()
                {
                    ChildIntegerProperty = 2,
                    ChildStringProperty = "I'm child json object"
                }
            };
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteJsonToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(initialContent);
            stringBuilder.AppendLine(content.ToJsonString(format));
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }

        #endregion 
    }
}
