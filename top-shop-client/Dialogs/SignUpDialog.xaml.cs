using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using top_shop_dbconnector;
using top_shop_models;

namespace top_shop_client.Dialogs
{
    public partial class SignUpDialog : Window, INotifyPropertyChanged
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
        public static DependencyProperty LoginProperty = DependencyProperty.Register(nameof(Login), typeof(string), typeof(SignUpDialog), new PropertyMetadata(string.Empty));
        public string ClientName
        {
            get => (string)GetValue(ClientNameProperty);
            set
            {
                SetValue(ClientNameProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClientName)));
            }
        }
        public static DependencyProperty ClientNameProperty = DependencyProperty.Register(nameof(ClientName), typeof(string), typeof(SignUpDialog), new PropertyMetadata(string.Empty));
        public string Phone
        {
            get => (string)GetValue(PhoneProperty);
            set
            {
                SetValue(PhoneProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Phone)));
            }
        }
        public static DependencyProperty PhoneProperty = DependencyProperty.Register(nameof(Phone), typeof(string), typeof(SignUpDialog), new PropertyMetadata(string.Empty));

        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly TopShopContext db;
        public SignUpDialog(TopShopContext db)
        {
            this.db = db;
            InitializeComponent();
        }

        public Client? Client { get; set; }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var potentialClient = db.Clients.FirstOrDefault(x => x.Username == Login);
            if (potentialClient is not null)
            {
                MessageBox.Show("Login is occupied");
                return;
            }
            Client = new()
            {
                Name = ClientName,
                Phone = Phone,
                Username = Login,
                Passhash = ComputeSha256Hash(PasswordBox.Password),
                Discount = 0
            };
            db.Add(Client);
            db.SaveChanges();
            MessageBox.Show("Suck sex");
            Close();
        }

        static string ComputeSha256Hash(string rawData) => 
            new(SHA256.HashData(Encoding.UTF8.GetBytes(rawData))
                .SelectMany(x => x.ToString("x2").ToArray()).ToArray());
    }
}