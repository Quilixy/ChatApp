using ChatApp.Models;
using System.Collections.ObjectModel;
using ChatApp.Services;
using ChatApp.Utilities;


namespace ChatApp.Views
{
    public partial class ChatPage : ContentPage
    {
        private readonly ChatService _chatService;
        private readonly UdpService _udpService;
        private Task<string> _chatId;
        private string _receiverUsername;
        public ObservableCollection<MessageModel> Messages { get; set; } = new ();

        public ChatPage( string receiverUsername)
        {
            InitializeComponent();
            _chatService = new ChatService();
            _udpService = new UdpService(_chatService);
            _udpService.StartListening();
            _receiverUsername = string.IsNullOrEmpty(receiverUsername) ? "Unknown" : receiverUsername;
            UserNameLabel.Text = $"Chatting with {_receiverUsername}";
            _chatId = App.DatabaseService.GetOrCreateChatIdAsync(App.CurrentUser.Username, _receiverUsername);
            
            if (App.CurrentUser == null)
            {
                AlertUtils.ShowAlertAsync("Hata", "Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.", "Tamam");
                Navigation.PushAsync(new LoginPage());
                return;
            }
            LoadMessages();
            BindingContext = this;
            
        }

        private async void LoadMessages( )
        {
            Messages = await _chatService.LoadMessagesAsync(await _chatId);
            MessagesListView.ItemsSource = Messages;
        }
        
        
        private async void OnSendClicked(object sender, EventArgs e)
        {
            string messageContent = MessageEntry.Text;
            string fullMessage = $"{App.CurrentUser.Username}:{messageContent}";
            await _udpService.SendUdpMessageAsync(fullMessage, _receiverUsername);
            //await _chatService.SendMessageAsync(App.CurrentUser.Username, _receiverUsername, messageContent);
            MessageEntry.Text = "";
            LoadMessages();
        }
        
    }
}