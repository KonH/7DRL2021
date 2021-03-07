using CloudBreak.State;

namespace CloudBreak.Service {
	public sealed class UIService {
		readonly UIState _state;

		public UIService(UIState state) {
			_state = state;
		}

		public void OpenMessage(Message message) {
			_state.ActiveMessage     = message;
			_state.ActivePanel.Value = UIPanel.MailMessage;
			message.Read.Value       = true;
		}

		public void CloseMessage() {
			_state.ActiveMessage     = null;
			_state.ActivePanel.Value = UIPanel.MailInbox;
		}
	}
}