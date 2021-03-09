using System;
using CloudBreak.State;
using UnityEngine;

namespace CloudBreak.View {
	[Serializable]
	public sealed class PanelObjectDictionary : SerializableDictionary<UIPanel, Panel> {}
}