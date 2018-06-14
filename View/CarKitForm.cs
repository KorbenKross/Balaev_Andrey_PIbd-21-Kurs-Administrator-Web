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
    public partial class CarKitForm : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICarKitService service;

        private int? id;

        private List<CarKitDetailViewModel> productComponents;


        public CarKitForm(ICarKitService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<CarKitDetailForm>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.CarKitId = id.Value;
                    }
                    productComponents.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<CarKitDetailForm>();
                form.Model = productComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    productComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        productComponents.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void LoadData()
        {
            try
            {
                if (productComponents != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = productComponents;
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

        private void ArticleForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CarKitViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.kit_name;
                        textBoxCount.Text = view.Count.ToString();
                        productComponents = view.CarKitDetails;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                productComponents = new List<CarKitDetailViewModel>();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (productComponents == null || productComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<CarKitDetailConnectingModel> productComponentBM = new List<CarKitDetailConnectingModel>();
                for (int i = 0; i < productComponents.Count; ++i)
                {
                    productComponentBM.Add(new CarKitDetailConnectingModel
                    {
                        Id = productComponents[i].Id,
                        CarKitId = productComponents[i].CarKitId,
                        DetailId = productComponents[i].DetailId,
                        Count = productComponents[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new CarKitConnectingModel
                    {
                        carkit_id = id.Value,
                        kit_name = textBoxName.Text,
                        Count = Convert.ToInt32(textBoxCount.Text),
                        CarKitDetails = productComponentBM
                    });
                }
                else
                {
                    service.AddElement(new CarKitConnectingModel
                    {
                        kit_name = textBoxName.Text,
                        Count = Convert.ToInt32(textBoxCount.Text),
                        CarKitDetails = productComponentBM
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
