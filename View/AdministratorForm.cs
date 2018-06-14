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
    public partial class AdministratorForm : Form
    {
        public AdministratorForm(IAdministratorService service)
        {
            InitializeComponent();
            this.service = service;
        }

        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IAdministratorService service;

        private int? id;

        private void CustomerForm_Load(object sender, EventArgs e)
        {
             if (id.HasValue)
            {
                try
                {
                    AdministratorViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxFIO.Text = view.name;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new AdministratorConnectingModel
                    {
                        admin_id = id.Value,
                        name = textBoxFIO.Text
                    });
                }
                else
                {
                    service.AddElement(new AdministratorConnectingModel
                    {
                        name = "andrey",
                        login = "andrey",
                        password = "123",
                        mail = "lalala"
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        
        }
    }
}
