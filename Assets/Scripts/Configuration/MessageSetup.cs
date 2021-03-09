using System;
using System.Collections.Generic;
using UnityEngine;

namespace CloudBreak.Configuration {
	[CreateAssetMenu]
	public sealed class MessageSetup : ScriptableObject {
		public enum TemplateId {
			None,
			ServerKey,
			MainStoryStart,
			MainStory1,
			MainStoryFinal,
			BackStory1,
			BackStoryN,
		}

		public sealed class Template {
			public readonly string Sender;
			public readonly string Receiver;
			public readonly string Header;
			public readonly string Body;

			public Template(string sender, string receiver, string header, string body) {
				Sender   = sender;
				Receiver = receiver;
				Header   = header;
				Body     = body;
			}
		}

		[SerializeField] TextAsset _sourceFile;

		Dictionary<TemplateId, Template> _templates;

		public IReadOnlyDictionary<TemplateId, Template> Templates => TryLoadTemplates();

		Dictionary<TemplateId, Template> TryLoadTemplates() {
			if ( _templates != null ) {
				return _templates;
			}
			_templates = new Dictionary<TemplateId, Template>();
			var lines      = _sourceFile.text.Split('\n');
			var templateId = TemplateId.None;
			var sender     = string.Empty;
			var receiver   = string.Empty;
			var header     = string.Empty;
			var body       = new List<string>();
			void AddTemplate() {
				_templates.Add(templateId, new Template(sender, receiver, header, string.Join("\n", body)));
			}
			foreach ( var line in lines ) {
				if ( string.IsNullOrWhiteSpace(line) ) {
					AddTemplate();
					templateId = TemplateId.None;
					sender     = string.Empty;
					receiver   = string.Empty;
					header     = string.Empty;
					body.Clear();
					continue;
				}
				if ( templateId == TemplateId.None ) {
					templateId = (TemplateId)Enum.Parse(typeof(TemplateId), line);
					continue;
				}
				if ( string.IsNullOrWhiteSpace(sender) ) {
					sender = line;
					continue;
				}
				if ( string.IsNullOrWhiteSpace(receiver) ) {
					receiver = line;
					continue;
				}
				if ( string.IsNullOrWhiteSpace(header) ) {
					header = line;
					continue;
				}
				body.Add(line);
			}
			if ( templateId != TemplateId.None ) {
				AddTemplate();
			}
			return _templates;
		}
	}
}