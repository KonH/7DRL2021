using CloudBreak.State;

namespace CloudBreak.Service {
	public sealed class UIService {
		readonly UIState _state;

		public UIService(UIState state) {
			_state = state;
		}

		public void OpenMessage(Message message) {
			_state.ActiveMessage = message;
			ChangePanel(UIPanel.MailMessage);
			message.Read.Value = true;
		}

		public void CloseMessage() {
			_state.ActiveMessage = null;
			ChangePanel(_state.PreviousPanel);
		}

		public void ChangePanel(UIPanel panel) {
			_state.PreviousPanel     = _state.ActivePanel.Value;
			_state.ActivePanel.Value = panel;
		}
	}
}