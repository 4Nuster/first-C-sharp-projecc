using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SQLite;

namespace test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class User
    {
        public string phone { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public static int idCounter = 0;
        public int id { get; }

        public User(string phone, string password, string email="") {
            this.phone = phone;
            this.password = password;
            this.email = email;
            //id = idCounter;
            //idCounter++;
        }

        public User(System.Int64 id, System.String phone, System.String email, System.String password)
        {
            this.id = (int) id;
            this.phone = phone;
            this.email = email;
            this.password = password;
        }
    }
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //List<User> users = new List<User>();
        int activeUser;

        private void signUp(object sender, RoutedEventArgs e)
        {
            if (password2Textbox.Text != passwordConfirmTextbox.Text)
            {
                passwordConfirmTextbox.Text = "PSW INCORRECT!";
            }
            else if(phone2Textbox.Text == "")
            {
                phone2Textbox.Text = "NO NUMBER!";
            }
            else
            {
                /*users.Add(new User(phone2Textbox.Text, password2Textbox.Text, emailTextbox.Text));
                activeUser = User.idCounter - 1;

                foreach(User user in users)
                {
                    if(user.id == activeUser)
                    {
                        userEmailBox.Text = user.email;
                        userPhoneBox.Text = user.phone;
                        userIdBox.Text = user.id.ToString();
                        break;
                    }
                }*/
                User user = CRUD.read(new User(phone2Textbox.Text, password2Textbox.Text));
                if (user == null)
                {
                    CRUD.create(new User(phone2Textbox.Text, password2Textbox.Text, emailTextbox.Text));
                    
                    user = CRUD.read(new User(phone2Textbox.Text, password2Textbox.Text));
                    activeUser = user.id;
                    userEmailBox.Text = user.email;
                    userPhoneBox.Text = user.phone;
                    userIdBox.Text = user.id.ToString();

                    profile.IsEnabled = true;
                    mainTabControl.SelectedItem = profile;
                    home.IsEnabled = false;

                    phone2Textbox.Text = "";
                    emailTextbox.Text = "";
                    password2Textbox.Text = "";
                    passwordConfirmTextbox.Text = "";
                    phoneTextbox.Text = "";
                    passwordTextbox.Text = "";
                }
                else
                {
                    MessageBox.Show("Phone number already taken!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                /*foreach (User user in users)
                {
                    Console.WriteLine("user " + user.id + ": " + user.phone + ", psw: " + user.password + ", email: "+ user.email);
                }
                Console.WriteLine("aand done");*/
            }
        }

        private void logIn(object sender, RoutedEventArgs e)
        {
            User user = CRUD.read(new User(phoneTextbox.Text, passwordTextbox.Text));
            if (user == null) MessageBox.Show("Incorrect credentials!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                activeUser = user.id;
                userEmailBox.Text = user.email;
                userPhoneBox.Text = user.phone;
                userIdBox.Text = user.id.ToString();

                profile.IsEnabled = true;
                mainTabControl.SelectedItem = profile;
                home.IsEnabled = false;

                phone2Textbox.Text = "";
                emailTextbox.Text = "";
                password2Textbox.Text = "";
                passwordConfirmTextbox.Text = "";
                phoneTextbox.Text = "";
                passwordTextbox.Text = "";
            }

            /*bool found = false;
            foreach (User user in users)
            {
                if (user.phone == phoneTextbox.Text)
                {
                    found = true;
                    if (user.password != passwordTextbox.Text)
                    {
                        passwordTextbox.Text = "PSW INCORRECT!";
                    }
                    else
                    {
                        activeUser = user.id;
                        userEmailBox.Text = user.email;
                        userPhoneBox.Text = user.phone;
                        userIdBox.Text = user.id.ToString();

                        profile.IsEnabled = true;
                        mainTabControl.SelectedItem = profile;
                        home.IsEnabled = false;

                        phone2Textbox.Text = "";
                        emailTextbox.Text = "";
                        password2Textbox.Text = "";
                        passwordConfirmTextbox.Text = "";
                        phoneTextbox.Text = "";
                        passwordTextbox.Text = "";
                    }
                    break;
                }
            }
            if (!found) phoneTextbox.Text = "PHONE INCORRECT!";*/
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            home.IsEnabled = true;
            mainTabControl.SelectedItem = home;
            profile.IsEnabled = false;
        }

        private void deleteUser(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete account?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                /*foreach(User user in users)
                {
                    if(user.id == activeUser)
                    {
                        users.Remove(user);
                        break;
                    }
                }*/
                CRUD.delete(activeUser);
                
                home.IsEnabled = true;
                mainTabControl.SelectedItem = home;
                profile.IsEnabled = false;
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            if(newPswBox.Text != newPswConfirmBox.Text)
            {
                newPswConfirmBox.Text = "PSW INCORRECT!";
            }
            else
            {
                User user = CRUD.readById(activeUser);
                if(user.password != oldPswBox.Text)
                {
                    oldPswBox.Text = "PSW INCORRECT";
                }
                else
                {
                    if (newPhoneBox.Text != "") user.phone = newPhoneBox.Text;
                    if (newEmailBox.Text != "") user.email = newEmailBox.Text;
                    if (newPswBox.Text != "") user.password = newPswBox.Text;

                    CRUD.update(user);
                }
                userEmailBox.Text = user.email;
                userPhoneBox.Text = user.phone;

                /*foreach(User user in users)
                {
                    if(user.id == activeUser)
                    {
                        if(oldPswBox.Text != user.password)
                        {
                            oldPswBox.Text = "PSW INCORRECT!";
                        }
                        else
                        {
                            if (newPhoneBox.Text != "") user.phone = newPhoneBox.Text;
                            if (newEmailBox.Text != "") user.email = newEmailBox.Text;
                            if (newPswBox.Text != "") user.password = newPswBox.Text;
                        }

                        userEmailBox.Text = user.email;
                        userPhoneBox.Text = user.phone;
                        userIdBox.Text = user.id.ToString();
                        break;
                    }
                }*/

            }
        }
    }
}
