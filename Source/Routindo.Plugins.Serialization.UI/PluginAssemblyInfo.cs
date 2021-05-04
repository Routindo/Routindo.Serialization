using Routindo.Contract.Attributes;
using Routindo.Plugins.Serialization.Components.Actions.Json;
using Routindo.Plugins.Serialization.Components.Actions.Text;
using Routindo.Plugins.Serialization.UI.Views;

[assembly: ComponentConfigurator(typeof(JsonToFileActionView), WriteJsonToFileAction.ComponentUniqueId, "Configurator for Json to File Serializer")]
[assembly: ComponentConfigurator(typeof(TextToFileActionView), WriteTextToFileAction.ComponentUniqueId, "Configurator for Text to File Serializer")]