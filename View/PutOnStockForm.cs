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
    public partial class PutOnStockForm : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStockService serviceS;

        private readonly IDetailService serviceC;

        private readonly IGeneralService serviceM;

        public PutOnStockForm(IStockService serviceS, IDetailService serviceC, IGeneralService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
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
            if (comboBoxResource.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.PutComponentOnStock(new StockDetailConnectingModel
                {
                    DetailId = Convert.ToInt32(comboBoxIngridient.SelectedValue),
                    StockId = Convert.ToInt32(comboBoxResource.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
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

        private void PutOnResource_Load(object sender, EventArgs e)
        {
            try
            {
                List<DetailViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxIngridient.DisplayMember = "detail_name";
                    comboBoxIngridient.ValueMember = "detail_id";
                    comboBoxIngridient.DataSource = listC;
                    comboBoxIngridient.SelectedItem = null;
                }
                List<StockViewModel> listS = serviceS.GetList();
                if (listS != null)
                {
                    comboBoxResource.DisplayMember = "StockName";
                    comboBoxResource.ValueMember = "StockId";
                    comboBoxResource.DataSource = listS;
                    comboBoxResource.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
