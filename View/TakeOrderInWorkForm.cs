﻿using Service.ConnectingModel;
using Service.LogicInterface;
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
    public partial class TakeOrderInWorkForm : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IGeneralService serviceM;

        private int? id;

        public TakeOrderInWorkForm(IGeneralService serviceM)
        {
            InitializeComponent();
            this.serviceM = serviceM;
        }

        private void TakeRequestInWorkForm_Load(object sender, EventArgs e)
        {
        //    try
        //    {
        //        if (!id.HasValue)
        //        {
        //            MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            Close();
        //        }
        //        List<WorkerViewModel> listI = serviceI.GetList();
        //        if (listI != null)
        //        {
        //            comboBoxWorker.DisplayMember = "WorkerFIO";
        //            comboBoxWorker.ValueMember = "Id";
        //            comboBoxWorker.DataSource = listI;
        //            comboBoxWorker.SelectedItem = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //if (comboBoxWorker.SelectedValue == null)
            //{
            //    MessageBox.Show("Выберите исполнителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //try
            //{
            //    serviceM.TakeOrderInWork(new OrderConnectingModel
            //    {
            //        Id = id.Value,
            //        WorkerId = Convert.ToInt32(comboBoxWorker.SelectedValue)
            //    });
            //    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    DialogResult = DialogResult.OK;
            //    Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.Cancel;
            //Close();

        }
    }
}
