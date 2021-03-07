using CloudBreak.View;
using CloudBreak.Configuration;
using UnityEditor;

namespace CloudBreak.Editor {
	[CustomPropertyDrawer(typeof(PanelObjectDictionary))]
	[CustomPropertyDrawer(typeof(MessageSetup.MessageTemplateDictionary))]
	public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
}
