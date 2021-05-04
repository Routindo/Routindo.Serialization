using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Routindo.Contract;
using Routindo.Contract.Arguments;
using Routindo.Contract.Services;
using Routindo.Plugins.Serialization.Components.Actions.ContentToFile;

namespace Routindo.Plugins.Serialization.Tests
{
    [TestClass]
    public class WriteContentToFileActionTests
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

        [TestMethod]
        [TestCategory("Integration Test")]
        public void WriteContentToFileActionTest()
        {
            WriteContentToFileAction action = new WriteContentToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteContentToFileAction)),
                FilePath = _filePath
                // ,Append = false
            };
            var content = "Hello world!";
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteContentToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            Assert.AreEqual(content, fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNoNewLineContentToFileActionTest()
        {
            WriteContentToFileAction action = new WriteContentToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteContentToFileAction)),
                FilePath = _filePath,
                Append = true
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = "Hello world!";
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteContentToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(initialContent);
            stringBuilder.Append(content);
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNewLineAfterContentToFileActionTest()
        { 
            WriteContentToFileAction action = new WriteContentToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteContentToFileAction)),
                FilePath = _filePath,
                Append = true, 
                NewLineBeforeAppend = false,
                NewLineAfterAppend = true,
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = "Hello world!";
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteContentToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(initialContent);
            stringBuilder.AppendLine(content);
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNewLineBeforeContentToFileActionTest()
        {
            WriteContentToFileAction action = new WriteContentToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteContentToFileAction)),
                FilePath = _filePath,
                Append = true,
                NewLineBeforeAppend = true,
                NewLineAfterAppend = false,
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = "Hello world!";
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteContentToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(initialContent);
            stringBuilder.Append(content);
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void AppendNewLineBeforeAfterContentToFileActionTest()
        {
            WriteContentToFileAction action = new WriteContentToFileAction()
            {
                Id = PluginUtilities.GetUniqueId(),
                LoggingService = ServicesContainer.ServicesProvider.GetLoggingService(nameof(WriteContentToFileAction)),
                FilePath = _filePath,
                Append = true,
                NewLineBeforeAppend = true,
                NewLineAfterAppend = true,
            };
            var initialContent = "First Line";
            File.WriteAllText(_filePath, initialContent);
            var content = "Hello world!";
            action.Execute(ArgumentCollection.New()
                .WithArgument(WriteContentToFileActionExecutionArgs.Content, content)
            );

            Assert.IsTrue(File.Exists(_filePath));
            var fileContent = File.ReadAllText(_filePath);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(initialContent);
            stringBuilder.AppendLine(content);
            Assert.AreEqual(stringBuilder.ToString(), fileContent);
        }
    }
}
