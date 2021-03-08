using CloudBreak.View;
using UnityEditor;

namespace CloudBreak.Editor {
	[CustomPropertyDrawer(typeof(PanelObjectDictionary))]
	public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
}
