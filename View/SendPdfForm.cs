using Service.ConnectingModel;
using Service.LogicInterface;
using Service.UserViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AbstractPizzeriaView
{
    public partial class SendPdfForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAdministratorService service;

        private readonly IGeneralService serviceG;

        public SendPdfForm(IAdministratorService service, IGeneralService serviceG)
        {
            InitializeComponent();
            this.service = service;
            this.serviceG = serviceG;
        }

        private void buttonSendMail_Click(object sender, EventArgs e)
        {
            string path = "";

            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    serviceG.SendPdf(sfd.FileName);
                    path = sfd.FileName;
                    MessageBox.Show("Уведомление отправлено", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            SendEmail(path, comboBoxMails.Text);
        }

        private void SendEmail(string path, string mail)
        {

            if (string.IsNullOrEmpty(mail))
            {
                throw new Exception("Элемент не найден");
            }

            if (!string.IsNullOrEmpty(mail))
            {
                if (!Regex.IsMatch(mail, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                {
                    throw new Exception("Элемент не соответствует формату почты");
                }
            }

            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            String filePath = "";

            try
            {
                filePath = Path.GetFullPath(path);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка");
            }


            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mail));
                objMailMessage.Subject = "Новая заявка";
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                string file = Path.GetFullPath(filePath);
                Attachment attach = new Attachment(file, MediaTypeNames.Application.Octet);
                objMailMessage.Attachments.Add(attach);

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]);

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }

        private void SendPdfForm_Load(object sender, EventArgs e)
        {
            List<AdministratorViewModel> list = service.GetList();
            if (list != null)
            {
                comboBoxMails.DisplayMember = "mail";
                comboBoxMails.ValueMember = "admin_id";
                comboBoxMails.DataSource = list;
                comboBoxMails.SelectedItem = null;
            }
        }
    }
}
