using System;
using System.Linq;
using System.Windows.Forms;
using SettlementLibrary;
using Microsoft.VisualBasic;
using System.Windows.Forms.Design;
using System.Text;

namespace SettlementManager
{
    public partial class Form1 : Form
    {
        private SettlementGroup<AdditionalData> settlementGroup = new SettlementGroup<AdditionalData>();
        private string? oldName = null;
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            UpdateDataGridView();

        }
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Name" && e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                oldName = row.Cells["Name"].Value?.ToString();
            }
        }
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var row = dataGridView1.Rows[e.RowIndex];
            string? name = row.Cells["Name"].Value?.ToString();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Settlement name cannot be empty.");
                return;
            }

            Settlement<AdditionalData>? settlement = null;
            if (oldName != null && oldName != name)
            {
                settlement = settlementGroup.GetAllSettlements().FirstOrDefault(s => s.Name == oldName);
                if (settlement != null)
                {
                    settlementGroup.RemoveSettlement(oldName);
                }
            }
            else
            {
                settlement = settlementGroup.GetAllSettlements().FirstOrDefault(s => s.Name == name);
            }

            if (settlement == null)
            {
                MessageBox.Show("Settlement with name " + name + " was not found.");
                return;
            }
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            try
            {
                switch (columnName)
                {
                    case "Name":
                        string? newName = row.Cells["Name"].Value?.ToString();
                        if (!string.IsNullOrWhiteSpace(newName))
                        {
                            if (settlementGroup.GetAllSettlements().Any(s => s.Name == newName))
                            {
                                MessageBox.Show("A settlement with this name already exists.");
                                row.Cells["Name"].Value = oldName;
                                return;
                            }

                            settlement.Name = newName;
                        }
                        else
                        {
                            MessageBox.Show("Name cannot be empty.");
                        }
                        break;
                    case "Region":
                        settlement.Region = row.Cells["Region"].Value?.ToString() ?? settlement.Region;
                        break;
                    case "Population":
                        if (int.TryParse(row.Cells["Population"].Value?.ToString(), out int population))
                        {
                            settlement.Population = population;
                        }
                        else
                        {
                            MessageBox.Show("Invalid population format.");
                        }
                        break;
                    case "Area":
                        if (double.TryParse(row.Cells["Area"].Value?.ToString(), out double area))
                        {
                            settlement.Area = area;
                        }
                        else
                        {
                            MessageBox.Show("Invalid area format.");
                        }
                        break;
                    case "Mayor":
                        settlement.AdditionalData.Mayor = row.Cells["Mayor"].Value?.ToString() ?? settlement.AdditionalData.Mayor;
                        break;
                    case "Founded":
                        if (DateTime.TryParse(row.Cells["Founded"].Value?.ToString(), out DateTime founded))
                        {
                            settlement.AdditionalData.Founded = founded;
                        }
                        else
                        {
                            MessageBox.Show("Invalid founded date format.");
                        }
                        break;
                }
                if (oldName != null && oldName != name)
                {
                    settlementGroup.AddSettlement(settlement);
                    oldName = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
        }

        private void InitializeDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = false;
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Name", "Name");
                dataGridView1.Columns.Add("Region", "Region");
                dataGridView1.Columns.Add("Population", "Population");
                dataGridView1.Columns.Add("Area", "Area");
                dataGridView1.Columns.Add("Mayor", "Mayor");
                dataGridView1.Columns.Add("Founded", "Founded");
            }
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;
        }

        private void UpdateDataGridView()
        {
            dataGridView1.Rows.Clear();
            foreach (var s in settlementGroup.GetAllSettlements())
            {
                dataGridView1.Rows.Add(
                    s.Name,
                    s.Region,
                    s.Population.ToString(),
                    s.Area.ToString(),
                    s.AdditionalData.Mayor,
                    s.AdditionalData.Founded.ToString("yyyy-MM-dd")
                );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string name = Interaction.InputBox("Enter settlement name:", "Add name", "");
                if (string.IsNullOrWhiteSpace(name)) return;

                string region = Interaction.InputBox("Enter region:", "Add region", "");
                if (string.IsNullOrWhiteSpace(region)) return;

                string populationStr = Interaction.InputBox("Enter population:", "Add population", "");
                if (string.IsNullOrWhiteSpace(populationStr)) return;
                if (!int.TryParse(populationStr, out int population))
                {
                    MessageBox.Show("Invalid population format.");
                    return;
                }

                string areaStr = Interaction.InputBox("Enter area (km²):", "Add area", "");
                if (string.IsNullOrWhiteSpace(areaStr)) return;
                if (!double.TryParse(areaStr, out double area))
                {
                    MessageBox.Show("Invalid area format.");
                    return;
                }

                string mayor = Interaction.InputBox("Enter mayor name:", "Add Settlement", "");
                if (string.IsNullOrWhiteSpace(mayor)) return;

                string foundedStr = Interaction.InputBox("Enter founded date (YYYY-MM-DD):", "Add Settlement", "");
                if (string.IsNullOrWhiteSpace(foundedStr)) return;
                if (!DateTime.TryParse(foundedStr, out DateTime founded))
                {
                    MessageBox.Show("Invalid date format. Use YYYY-MM-DD.");
                    return;
                }

                AdditionalData additionalData = new AdditionalData
                {
                    Mayor = mayor,
                    Founded = founded
                };

                Settlement<AdditionalData> settlement = new Settlement<AdditionalData>(name, region, population, area, additionalData);
                settlementGroup.AddSettlement(settlement);

                MessageBox.Show("Settlement added successfully!");
                UpdateDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding settlement: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select a row to delete.");
                    return;
                }

                var row = dataGridView1.SelectedRows[0];
                string? name = row.Cells["Name"].Value?.ToString();
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Cannot determine the settlement name for deletion.");
                    return;
                }

                settlementGroup.RemoveSettlement(name);
                MessageBox.Show("Settlement deleted successfully!");
                UpdateDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deletion: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text files (*.txt)|*.txt|JSON files (*.json)|*.json|All files (*.*)|*.*",
                    Title = "Choose or make file"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;


                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Dispose();
                    }

                    settlementGroup.SaveToFile(filePath);
                    MessageBox.Show("Data saved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Text files (*.txt)|*.txt|JSON files (*.json)|*.json|All files (*.*)|*.*",
                    Title = "Choose file"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    settlementGroup.LoadFromFile(openFileDialog.FileName);
                    MessageBox.Show("Data loaded successfully!");
                    UpdateDataGridView();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void byNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settlementGroup.SortByName();
            UpdateDataGridView();
        }

        private void byPopulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settlementGroup.SortByPopulation();
            UpdateDataGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string criteriaInput = Interaction.InputBox(
                    "Select criteria to search by:\n" +
                    "1. Name\n" +
                    "2. Region\n" +
                    "3. Population\n" +
                    "4. Area\n" +
                    "5. Mayor\n" +
                    "6. Founded Date\n" +
                    "Enter the numbers of the criteria and use commas:",
                    "Choose Search Criteria",
                    "");

                if (string.IsNullOrWhiteSpace(criteriaInput))
                {
                    MessageBox.Show("You did not select any criteria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var criteriaNumbers = criteriaInput.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                                   .Select(s => s.Trim())
                                                   .ToList();

                bool searchByName = false;
                bool searchByRegion = false;
                bool searchByPopulation = false;
                bool searchByArea = false;
                bool searchByMayor = false;
                bool searchByFounded = false;

                foreach (var numStr in criteriaNumbers)
                {
                    switch (numStr)
                    {
                        case "1":
                            searchByName = true;
                            break;
                        case "2":
                            searchByRegion = true;
                            break;
                        case "3":
                            searchByPopulation = true;
                            break;
                        case "4":
                            searchByArea = true;
                            break;
                        case "5":
                            searchByMayor = true;
                            break;
                        case "6":
                            searchByFounded = true;
                            break;
                        default:
                            MessageBox.Show("Invalid criteria number: " + numStr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }
                }
                string nameCriteria = "";
                string regionCriteria = "";
                int populationCriteria = 0;
                double areaCriteria = 0;
                string mayorCriteria = "";
                DateTime foundedCriteria = DateTime.MinValue;
                if (searchByName)
                {
                    nameCriteria = Interaction.InputBox("Enter the settlement's name to search for:", "Name", "");
                    if (string.IsNullOrWhiteSpace(nameCriteria))
                    {
                        MessageBox.Show("Name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (searchByRegion)
                {
                    regionCriteria = Interaction.InputBox("Enter the region to search for:", "Region", "");
                    if (string.IsNullOrWhiteSpace(regionCriteria))
                    {
                        MessageBox.Show("Region cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (searchByPopulation)
                {
                    string populationStr = Interaction.InputBox("Enter the population to search for:", "Population", "");
                    if (string.IsNullOrWhiteSpace(populationStr))
                    {
                        MessageBox.Show("Population cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!int.TryParse(populationStr, out populationCriteria))
                    {
                        MessageBox.Show("Population must be an integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (searchByArea)
                {
                    string areaStr = Interaction.InputBox("Enter the area (km²) to search for:", "Area", "");
                    if (string.IsNullOrWhiteSpace(areaStr))
                    {
                        MessageBox.Show("Area cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!double.TryParse(areaStr, out areaCriteria))
                    {
                        MessageBox.Show("Area must be a number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (searchByMayor)
                {
                    mayorCriteria = Interaction.InputBox("Enter the mayor's name to search for:", "Mayor", "");
                    if (string.IsNullOrWhiteSpace(mayorCriteria))
                    {
                        MessageBox.Show("Mayor's name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (searchByFounded)
                {
                    string foundedStr = Interaction.InputBox("Enter the founded date (YYYY-MM-DD) to search for:", "Founded Date", "");
                    if (string.IsNullOrWhiteSpace(foundedStr))
                    {
                        MessageBox.Show("Founded date cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!DateTime.TryParse(foundedStr, out foundedCriteria))
                    {
                        MessageBox.Show("Founded date must be in a valid date format (e.g., YYYY-MM-DD).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                var matchingSettlements = settlementGroup.GetAllSettlements().Where(settlement =>
                    (!searchByName || settlement.Name.IndexOf(nameCriteria, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (!searchByRegion || settlement.Region.IndexOf(regionCriteria, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (!searchByPopulation || settlement.Population == populationCriteria) &&
                    (!searchByArea || Math.Abs(settlement.Area - areaCriteria) < 0.0001) && 
                    (!searchByMayor || settlement.AdditionalData.Mayor.IndexOf(mayorCriteria, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (!searchByFounded || settlement.AdditionalData.Founded.Date == foundedCriteria.Date)
                ).ToList();

                if (matchingSettlements.Count == 0)
                {
                    MessageBox.Show("No matching settlements found.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    StringBuilder result = new StringBuilder();
                    result.AppendLine("Matching Settlements:");
                    foreach (var settlement in matchingSettlements)
                    {
                        result.AppendLine($"Name: {settlement.Name}, Region: {settlement.Region}, Population: {settlement.Population}, " +
                                          $"Area: {settlement.Area} km², Mayor: {settlement.AdditionalData.Mayor}, " +
                                          $"Founded: {settlement.AdditionalData.Founded:yyyy-MM-dd}");
                    }
                    MessageBox.Show(result.ToString(), "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}