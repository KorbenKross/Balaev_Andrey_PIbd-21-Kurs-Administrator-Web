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
    public partial class CarKitDetailForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public CarKitViewModel Model { set { model = value; } get { return model; } }

        private readonly IDetailService service;

        private CarKitViewModel model;

        public CarKitDetailForm(IDetailService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void ArticleIngridientForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<DetailViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxIngridient.DisplayMember = "detail_name";
                    comboBoxIngridient.ValueMember = "detail_id";
                    comboBoxIngridient.DataSource = list;
                    comboBoxIngridient.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxIngridient.Enabled = false;
                comboBoxIngridient.SelectedValue = model.DetailId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngridient.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new CarKitViewModel
                    {
                        DetailId = Convert.ToInt32(comboBoxIngridient.SelectedValue),
                        DetailName = comboBoxIngridient.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
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
