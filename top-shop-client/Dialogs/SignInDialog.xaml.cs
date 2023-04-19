using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using top_shop_dbconnector;
using top_shop_models;

namespace top_shop_client.Dialogs
{
    public partial class SignInDialog : Window, INotifyPropertyChanged
    {
        public string Login
        {
            get => (string)GetValue(LoginProperty); 
            set
            {
                SetValue(LoginProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Login))); 
            }
        }
        public static DependencyProperty LoginProperty = DependencyProperty.Register(nameof(Login), typeof(string), typeof(SignInDialog), new PropertyMetadata(string.Empty));

        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly TopShopContext db;
        public SignInDialog(TopShopContext db)
        {
            this.db = db;
            InitializeComponent();
        }

        public Client? Client { get; set; }
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            var potentialClient = db.Clients.FirstOrDefault(x => x.Username == Login && x.Passhash == ComputeSha256Hash(PasswordBox.Password));
            if (potentialClient is null)
            {
                MessageBox.Show("Wrong User");
                return;
            }
            Client = potentialClient;
            Close();
        }

        static string ComputeSha256Hash(string rawData) => 
            new(SHA256.HashData(Encoding.UTF8.GetBytes(rawData))
                .SelectMany(x => x.ToString("x2").ToArray()).ToArray());
    }
}