using Service.ConnectingModel;
using Service.LogicInterface;
using Service.UserViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;
namespace View
{
    public partial class LoginForm : Form
    {

        public LoginForm(ISupplierService service)
        {
            InitializeComponent();
            this.service = service;
        }

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ISupplierService service;

        private void regButton_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<RegistrationForm>();
            form.ShowDialog();
        }

        private void entryButton_Click(object sender, EventArgs e)
        {
            try
            {
                service.LogIn(new SupplierConnectingModel
                {
                    login = textBoxLogin.Text,
                    password = textBoxPassword.Text
                });
                statusLabel.Text = "Вход выполнен";
                var form = Container.Resolve<MainForm>();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Ошибка входа";
            }
        }
    }
}
