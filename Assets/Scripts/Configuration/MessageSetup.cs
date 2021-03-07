using System;
using System.Collections.Generic;
using UnityEngine;

namespace CloudBreak.Configuration {
	[CreateAssetMenu]
	public sealed class MessageSetup : ScriptableObject {
		public enum TemplateId {
			Hello = 1,
		}

		[Serializable]
		public sealed class Template {
			public string Header;
			public string Body;
		}

		[Serializable]
		public sealed class MessageTemplateDictionary : SerializableDictionary<TemplateId, Template> {}

		[SerializeField] MessageTemplateDictionary _templates;

		public IReadOnlyDictionary<TemplateId, Template> Templates => _templates;
	}
}