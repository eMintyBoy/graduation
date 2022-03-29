using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace EconomicalCalculator
{
    public partial class CalculationForm : Form
    {
        public CalculationForm()
        {
            InitializeComponent();
        }

        private void GetHistory()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(dbConnection.connString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT * FROM история";
                cmd.ExecuteNonQuery();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                conn.Close();
                adapter.Fill(table);
                this.dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddCalculation(double co)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(dbConnection.connString);
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO история VALUES (null, @co)";
                cmd.Parameters.AddWithValue("co", co);
                cmd.ExecuteNonQuery();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                conn.Close();
                adapter.Fill(table);
                this.dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_WOC1_Click(object sender, EventArgs e)
        {
            GetHistory();
        }

        private void button_WOC2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_WOC3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            double v0 = double.Parse(textBox1.Text) * double.Parse(maskedTextBox1.Text);
            double to = Math.Round(double.Parse(textBox16.Text) * (1 + double.Parse(maskedTextBox6.Text)), 2);
            double szm = Math.Round(double.Parse(textBox4.Text) * double.Parse(maskedTextBox2.Text), 2);
            double soz = Math.Round((szm / 21) * to * double.Parse(maskedTextBox3.Text) * double.Parse(maskedTextBox4.Text) * double.Parse(textBox2.Text)*double.Parse(textBox2.Text),2);
            double sdz = Math.Round((soz * double.Parse(textBox7.Text)) / 100, 2);
            double sfcnz = Math.Round(((soz + sdz) * 0.34) / 100, 2);
            double sbgs = Math.Round(((soz + sdz) * 0.3) / 100, 2);
            double sm = Math.Round(double.Parse(maskedTextBox7.Text) * (v0 / 100), 2);
            double smv = Math.Round(double.Parse(maskedTextBox5.Text) * (v0 / 100) * 12, 2);
            double spz = Math.Round((soz * (double.Parse(textBox11.Text) / 100)) / 100, 2);
            double snr = Math.Round((soz * (double.Parse(textBox12.Text)) / 100) / 100, 2);
            double sr = Math.Round(soz + sdz + sfcnz + sbgs + sm + smv + spz + snr, 2);
            double srsa = Math.Round((sr * (double.Parse(textBox13.Text)) / 100) / 100, 2);
            double sp = Math.Round(sr + srsa, 2);
            double pps = Math.Round((sp * (double.Parse(textBox14.Text) / 100)) / 100, 2);
            double cp = Math.Round(sp + pps, 2);
            double nds = Math.Round((cp * 0.2) / 100, 2);
            double co = Math.Round(cp + nds, 2);

            richTextBox1.Text = "Скорректированный объем функций: " + v0.ToString() + "\n"
                + "Трудоемкость выполняемой работы: " + to.ToString() + "\n"
                + "Основная заработная плата: " + szm.ToString() + "\n"
                + "Основная заработная плата исполнителей: " + soz.ToString() + "\n"
                + "Дополнительная заработная плата: " + sdz.ToString() + "\n"
                + "Отчисления в Фонд социльной защиты населения: " + sfcnz.ToString() + "\n"
                + "Отчисления по обязательному страхованию от несчастных случаев на производстве и профессиональных заболеваний : " + sbgs.ToString() + "\n"
                + "Расходы на материалы: " + sm.ToString() + "\n"
                + "Расходы на оплату машинного времени: " + smv.ToString() + "\n"
                + "Прочие прямые затраты: " + spz.ToString() + "\n"
                + "Общепроизводственные и общехозяйственные (накладные) расходы: " + snr.ToString() + "\n"
                + "Сумма расходов на разработку ПС ВТ: " + sr.ToString() + "\n"
                + "Расходы на сопровождение и адаптацию: " + srsa.ToString() + "\n"
                + "Полная себестоимость ПС ВТ: " + sp.ToString() + "\n"
                + "Прибыль: " + pps.ToString() + "\n"
                + "Пронозируемая цена: " + cp.ToString() + "\n"
                + "Налог на добавленную стоимость: " + nds.ToString() + "\n"
                + "Прогнозируемая отпускная цена: " + co.ToString();

            AddCalculation(co);
        }
    }
}