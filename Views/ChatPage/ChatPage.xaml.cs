using ChatApp.Models;
using System.Collections.ObjectModel;
using ChatApp.Services;


namespace ChatApp.Views
{
    public partial class ChatPage : ContentPage
    {
        private readonly ChatService _chatService;
        private Task<string> _chatId;
        private string _receiverUsername;
        public ObservableCollection<MessageModel> Messages { get; set; } = new ();

        public ChatPage( string receiverUsername)
        {
            InitializeComponent();
            _chatService = new ChatService();
            _receiverUsername = string.IsNullOrEmpty(receiverUsername) ? "Unknown" : receiverUsername;
            UserNameLabel.Text = $"Chatting with {_receiverUsername}";
            _chatId = App.DatabaseService.GetOrCreateChatIdAsync(App.CurrentUser.Username, _receiverUsername);//chatId;
            //_chatId = new Random().NextInt64().ToString();
            
            if (App.CurrentUser == null)
            {
                DisplayAlert("Hata", "Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.", "Tamam");
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
            if (string.IsNullOrEmpty(messageContent) && App.CurrentUser == null)
            {
                DisplayAlert("Hata", "Mesaj veya kullanıcı bilgisi alınamadı.", "Tamam");
            }
            await _chatService.SendMessageAsync(App.CurrentUser.Username, _receiverUsername, messageContent);
            MessageEntry.Text = "";
            LoadMessages();
        }
    }
}