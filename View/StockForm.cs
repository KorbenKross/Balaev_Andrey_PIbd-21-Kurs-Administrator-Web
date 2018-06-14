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
    public partial class StockForm : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IStockService service;

        private int? id;

        public StockForm(IStockService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void ResourceForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    StockViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.StockName;
                        dataGridView.DataSource = view.StockDetail;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].Visible = false;
                        dataGridView.Columns[2].Visible = false;
                        dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new StockConnectingModel
                    {
                        StockId = id.Value,
                        StockName = textBoxName.Text
                    });
                }
                else
                {
                    service.AddElement(new StockConnectingModel
                    {
                        StockName = textBoxName.Text
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
