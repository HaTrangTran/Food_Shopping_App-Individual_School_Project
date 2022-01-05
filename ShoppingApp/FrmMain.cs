using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoppingApp
{
    public partial class FrmMain : Form
    {
        public string MyCellValue;
        int IngredientID;
        int CategoryID;
        int RecipeID;
        int RecipeID_Ing;

        public FrmMain()
        {
            InitializeComponent();
        }
       
        private void FrmMain_Load(object sender, EventArgs e)
        {
            DBStartUp();
            DefaultViewStartUp();
        }

        private void DBStartUp()
        {
            LoadDB Ldb = new LoadDB();
           
            Ldb.GetDataToGrdPlanMeal(GrdPlanMeal);

            Ldb.GetDataToGrdShoppingList(GrdShoppingList);

            Ldb.GetDataToGrdSupplyItems(GrdSupItems);

            Ldb.GetDatatoCombIngName(cbInsertIngName);

            Ldb.GetDatatoCombCategoryName(cbInsertIngCategory);

            Ldb.GetDatatoLstRecipeName(lstRecipeName);

            Ldb.GetDataToGrdPlanMeal(GrdPlanMeal_Confirm);

            Ldb.GetDataToGrdAccounting(GrdAccounting);

            Ldb.GetDataToGrdRecipeCategory(GrdRecipeCategory);

            Ldb.GetDataToGrdRecipeList(GrdRecipeList);

            Ldb.GetDatatoCbAdminCategoryName(CbAdminCategoryName);

            Ldb.GetDataToGrdIngredients(GrdIngredients);

            Ldb.GetDatatoCbAdminRecipeName(CbAdminRecipeName);

            //Ldb.GetDatatoCbAdminSelectIngre(CbAdminSelectIngre);
            Ldb.GetDatatoCbLocation(cbSelectLocation);


        }

        private void DefaultViewStartUp()
        {
            //-------------------------------SET CONTROLS (Tab Create new Plan meal)........
            CbRecipeCategory.Enabled = false;
            CbRecipe.Enabled = false;
            NumPortion.Enabled = false;
            BtAddToPlan.Enabled = false;
            btRemovePlanmeal.Enabled = false;
            BtCreateShoppingList.Enabled = false;
            btDelCategory.Enabled = false;
            btEditCategory.Enabled = false;
            BtEditRecipe.Enabled = false;
            BtDeleteRecipe.Enabled = false;
            //btAccounting.Enabled = false;

            txtInsertIngrLocation.Text = "";
            txtUpdateIngLocation.Text = "";
            txtUpdateIngCategory.Text = "";

            GrdPlanMeal.Columns[0].Width = 330;
            GrdPlanMeal.Columns[1].Width = 130;
            GrdPlanMeal.Columns[2].Width = 130;

            GrdShoppingList.Columns[0].Width = 340;
            GrdShoppingList.Columns[1].Width = 130;
            GrdShoppingList.Columns[2].Width = 130;


            //-------------------------------SET COLUMN WIDTH AND CONTROLS(Tab Suplpy Items)-------

            radioSelectIngName.Checked = true;
            radioSelectIngCategory.Checked = true;
            GrdSupItems.Columns[0].Width = 130;
            GrdSupItems.Columns[1].Width = 250;
            GrdSupItems.Columns[2].Width = 100;
            GrdSupItems.Columns[3].Width = 80;
            GrdSupItems.Columns[4].Width = 150;
            GrdSupItems.Columns[5].Width = 150;
            GrdSupItems.Columns[6].Width = 100;
            GrdSupItems.Columns[7].Width = 180;
            GrdSupItems.Columns[8].Width = 200;

            //-------------------------------SET COLUMN WIDTH AND CONTROLS (Tab Portion confirm)-------
            btConfirmCooked.Enabled = false;
            GrdPlanMeal_Confirm.Columns[0].Width = 480;
            GrdPlanMeal_Confirm.Columns[1].Width = 150;
            GrdPlanMeal_Confirm.Columns[2].Width = 150;
            //GrdPlanMeal.Columns[3].Width = 130;

            //-------------------------------SET COLUMN WIDTH AND CONTROLS (Tab Counting)-----------
            GrdAccounting.Columns[0].Width = 150;
            GrdAccounting.Columns[1].Width = 70;
            GrdAccounting.Columns[2].Width = 70;
            GrdAccounting.Columns[3].Width = 130;
            GrdAccounting.Columns[4].Width = 130;
            GrdAccounting.Columns[5].Width = 60;
            GrdAccounting.Columns[6].Width = 100;
            GrdAccounting.Columns[7].Width = 80;

            //-------------------------------SET COLUMN WIDTH AND CONTROLS (Tab Admin/Category)-----------
            GrdRecipeCategory.Columns[0].Width = 170;
            GrdRecipeCategory.Columns[1].Width = 520;

            //-------------------------------SET COLUMN WIDTH AND CONTROLS (Tab Admin/Recipe)-----------
            GrdRecipeList.Columns[0].Width = 100;
            GrdRecipeList.Columns[1].Width = 160;
            GrdRecipeList.Columns[2].Width = 100;
            GrdRecipeList.Columns[3].Width = 210;
            GrdRecipeList.Columns[4].Width = 130;

            //-------------------------------SET COLUMN WIDTH AND CONTROLS (Tab Admin/Ingredients)-----------
            GrdIngredients.Columns[0].Width = 100;
            GrdIngredients.Columns[1].Width = 360;
            GrdIngredients.Columns[2].Width = 120;
            GrdIngredients.Columns[3].Width = 120;
            RdAdminSelectIngre.Checked = true;
            btDeleteIngre.Enabled = false;
            btEditIngre.Enabled = false;


            //------------------------------------MAIN SCREEN WIDTH-------------------------
            this.Height = 930;
            this.Width = 1568;
            this.MaximizeBox = false;
            this.Text = "Shopping Plan";
        }

        private void FrmMain_ResizeEnd(object sender, EventArgs e)
        {
            this.Height = 930;
            this.Width = 1568;
        }

        private void btNewPlanMeal_Click(object sender, EventArgs e)
        {
            ExcuDB excu = new ExcuDB();
            excu.CreateNewPlan();
            LoadDB Ldb = new LoadDB();
            Ldb.GetDatatoCbRecipeCategory(CbRecipeCategory);
            Ldb.GetDataToGrdPlanMeal(GrdPlanMeal);
            Ldb.GetDataToGrdShoppingList(GrdShoppingList);
            CbRecipeCategory.Enabled = true;

        }

        private void CbRecipeCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CbRecipe.Enabled = true;
            int MyID = Convert.ToInt32(CbRecipeCategory.SelectedValue);
            LoadDB Ldb = new LoadDB();
            Ldb.GetDatatoCbRecipe(CbRecipe, MyID);
        }

        private void CbRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            NumPortion.Enabled = true;

        }

        private void NumPortion_ValueChanged(object sender, EventArgs e)
        {
            BtAddToPlan.Enabled = true;
        }

        private void BtAddToPlan_Click(object sender, EventArgs e)
        {
            LoadDB Ldb = new LoadDB();
            ExcuDB ex = new ExcuDB();
            ex.AddItemToPlan(Convert.ToInt32(CbRecipe.SelectedValue), CbRecipe.Text.Trim(), Convert.ToInt32(NumPortion.Value));

            Ldb.GetDataToGrdPlanMeal(GrdPlanMeal);
            Ldb.GetDatatoLstRecipeName(lstRecipeName);
            Ldb.GetDataToGrdPlanMeal(GrdPlanMeal_Confirm);
            BtCreateShoppingList.Enabled = true;
        }

        private void BtCreateShoppingList_Click(object sender, EventArgs e)
        {
            ExcuDB excu = new ExcuDB();
            excu.CreatShoppingList();
            LoadDB Ldb = new LoadDB();
            Ldb.GetDataToGrdShoppingList(GrdShoppingList);
        }

        private void btRemovePlanmeal_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            ex.RemoveItemFromPlan(MyCellValue);
            btRemovePlanmeal.Enabled = false;
            LoadDB Ldb = new LoadDB();
            Ldb.GetDataToGrdPlanMeal(GrdPlanMeal);
            Ldb.GetDatatoLstRecipeName(lstRecipeName);
            Ldb.GetDataToGrdPlanMeal(GrdPlanMeal_Confirm);
            btRemovePlanmeal.Enabled = false;
        }

        private void GrdPlanMeal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MyCellValue = GrdPlanMeal.CurrentRow.Cells["RecipeName"].Value.ToString();
                btRemovePlanmeal.Enabled = true;
            }
            catch
            {
                MessageBox.Show("No data in blank row!");
            }
        }

        private void lstRecipeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            btConfirmCooked.Enabled = true;
        }

        private void btConfirmCooked_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            ex.ConfirmUsedPortion(Convert.ToInt32(lstRecipeName.SelectedValue), lstRecipeName.Text.Trim(), Convert.ToInt32(numPortionCooked.Value));
            LoadDB ldb = new LoadDB();
            ldb.GetDataToGrdPlanMeal(GrdPlanMeal_Confirm);
            ldb.GetDataToGrdPlanMeal(GrdPlanMeal);
            ldb.GetDataToGrdSupplyItems(GrdSupItems);
            btConfirmCooked.Enabled = false;
        }

        private void btInsertItem_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            ex.InsertDataToSupplyItems(cbInsertIngName.SelectedValue.ToString().Trim(), cbInsertIngCategory.SelectedValue.ToString().Trim(), txtInsertIngName.Text.Trim(), txtInsertIngCategory.Text.Trim(), txtInsertIngUnit.Text.Trim(), Convert.ToInt32(numInsertIngNet.Value), Convert.ToDecimal(txtInsertIngPrice.Text), txtInsertIngrLocation.Text.Trim(), DtInsertIngSupDate.Value.ToString(), DtInsertIngExpDate.Value.ToString(), radioSelectIngName.Checked, radioSelectIngCategory.Checked, radioTypeIngName.Checked, radioTypeIngCategory.Checked);
            LoadDB ldb = new LoadDB();
            ldb.GetDataToGrdSupplyItems(GrdSupItems);
            ldb.GetDatatoCombIngName(cbInsertIngName);
            ldb.GetDatatoCombCategoryName(cbInsertIngCategory);
            ldb.GetDataToGrdAccounting(GrdAccounting);
        }

        private void GrdSupItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LoadDB Ldb = new LoadDB();
                IngredientID = Convert.ToInt32(GrdSupItems.CurrentRow.Cells["IngredientID"].Value);
                txtUpdateIngName.Text = GrdSupItems.CurrentRow.Cells["IngredientName"].Value.ToString();
                txtUpdateIngCategory.Text = GrdSupItems.CurrentRow.Cells["IngreCategory"].Value.ToString();
                txtUpdateIngUnit.Text = GrdSupItems.CurrentRow.Cells["Unit"].Value.ToString();
                numUpdateIngNet.Value = Convert.ToInt32(GrdSupItems.CurrentRow.Cells["Quantity"].Value);
                txtUpdateIngPrice.Text = GrdSupItems.CurrentRow.Cells["Price"].Value.ToString();
                DtUpdateIngSupDate.Value = Convert.ToDateTime(GrdSupItems.CurrentRow.Cells["DateOfArrival"].Value);
                DtUpdateIngExpDate.Value = Convert.ToDateTime(GrdSupItems.CurrentRow.Cells["ExpireDate"].Value);
                txtUpdateIngLocation.Text = Ldb.GetLocationName(IngredientID);
                
                btDeleteItem.Enabled = true;
                btUpdateItem.Enabled = true;
            }
            catch
            {
                MessageBox.Show("No data in blank row!");

            }
        }

        private void btDeleteItem_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.DeleteItemFromSupplyItems(IngredientID);
            ldb.GetDataToGrdSupplyItems(GrdSupItems);
            btUpdateItem.Enabled = false;
            btDeleteItem.Enabled = false;
        }

        private void btUpdateItem_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.UpdateItemFromSupplyItems(IngredientID, txtUpdateIngName.Text.Trim(), Convert.ToInt32(numUpdateIngNet.Value), txtUpdateIngUnit.Text.Trim(), txtUpdateIngLocation.Text.Trim(), DtUpdateIngSupDate.Value.ToString(), DtUpdateIngExpDate.Value.ToString(), Convert.ToDecimal(txtUpdateIngPrice.Text), txtUpdateIngCategory.Text.Trim());
            ldb.GetDataToGrdSupplyItems(GrdSupItems);
            ldb.GetDataToGrdAccounting(GrdAccounting);
            ldb.GetDatatoCombIngName(cbInsertIngName);
            ldb.GetDatatoCombCategoryName(cbInsertIngCategory);
            ldb.GetDatatoCbLocation(cbSelectLocation);
            btUpdateItem.Enabled = false;
            btDeleteItem.Enabled = false;
        }

        private void txtUpdateIngPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void txtInsertIngPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void btRefreshPlannedMeal_Click(object sender, EventArgs e)
        {
            LoadDB ldb = new LoadDB();
            ldb.GetDatatoLstRecipeName(lstRecipeName);
            ldb.GetDataToGrdPlanMeal(GrdPlanMeal_Confirm);
        }

        private void btAccounting_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            ex.Accounting(GrdAccounting, DtFrom.Value.ToString(), DtTo.Value.ToString());
            LbTotalCost.Text = ex.cost;
        }

        //-----------------------------------------------ADMIN_CATEGORY------------------------------------------------------

        private void btAddCategory_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.AddCategory_Admin(txtCateName.Text.Trim());
            ldb.GetDataToGrdRecipeCategory(GrdRecipeCategory);
            ldb.GetDatatoCbRecipeCategory(CbRecipeCategory);
            ldb.GetDatatoCbAdminCategoryName(CbAdminCategoryName);
        }

        private void GrdRecipeCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtCateName.Text = GrdRecipeCategory.CurrentRow.Cells["CategoryName"].Value.ToString();
                CategoryID = Convert.ToInt32(GrdRecipeCategory.CurrentRow.Cells["CategoryID"].Value);
                btEditCategory.Enabled = true;
                btDelCategory.Enabled = true;
            }
            catch
            {
                MessageBox.Show("No data in blank row!");

            }
        }

        private void btEditCategory_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.UpdateCategory_Admin(CategoryID, txtCateName.Text.Trim());
            ldb.GetDataToGrdRecipeCategory(GrdRecipeCategory);
            ldb.GetDatatoCbRecipeCategory(CbRecipeCategory);
            ldb.GetDatatoCbAdminCategoryName(CbAdminCategoryName);

            btEditCategory.Enabled = false;
            btDelCategory.Enabled = false;
        }

        private void btDelCategory_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.DelCategory_Admin(CategoryID);
            ldb.GetDataToGrdRecipeCategory(GrdRecipeCategory);
            ldb.GetDatatoCbRecipeCategory(CbRecipeCategory);
            ldb.GetDatatoCbAdminCategoryName(CbAdminCategoryName);

            btEditCategory.Enabled = false;
            btDelCategory.Enabled = false;
        }

        //-----------------------------------------------ADMIN_RECIPE------------------------------------------------------


        private void BtAddNewRecipe_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.AddRecipe_Admin(txtAdminRecipeName.Text.Trim(), Convert.ToInt32(CbAdminCategoryName.SelectedValue), txtAdminInstruction.Text, txtAdminEstimatedTime.Text.Trim());
            ldb.GetDataToGrdRecipeList(GrdRecipeList);
            ldb.GetDatatoCbAdminCategoryName(CbAdminCategoryName);
            ldb.GetDatatoCbAdminRecipeName(CbAdminRecipeName);
        }

        private void GrdRecipeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LoadDB ldb = new LoadDB();

                RecipeID = Convert.ToInt32(GrdRecipeList.CurrentRow.Cells["RecipeID"].Value);
                txtAdminRecipeName.Text = GrdRecipeList.CurrentRow.Cells["RecipeName"].Value.ToString();
                CategoryID = Convert.ToInt32(GrdRecipeList.CurrentRow.Cells["CategoryID"].Value);
                CbAdminCategoryName.Text = ldb.GetCateNametoCb_RecAdmin(CategoryID);
                CbAdminCategoryName.SelectedValue = CategoryID.ToString();
                txtAdminInstruction.Text = GrdRecipeList.CurrentRow.Cells["Instructions"].Value.ToString();
                txtAdminEstimatedTime.Text = GrdRecipeList.CurrentRow.Cells["EstimatedTime"].Value.ToString();
                BtEditRecipe.Enabled = true;
                BtDeleteRecipe.Enabled = true;
            }
            catch
            {
                MessageBox.Show("No data in blank row!");
            }
        }

        private void BtEditRecipe_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.UpdateRecipe_Admin(RecipeID, txtAdminRecipeName.Text.Trim(), Convert.ToInt32(CbAdminCategoryName.SelectedValue), txtAdminInstruction.Text, txtAdminEstimatedTime.Text.Trim());
            ldb.GetDataToGrdRecipeList(GrdRecipeList);
            ldb.GetDatatoCbAdminCategoryName(CbAdminCategoryName);
            ldb.GetDatatoCbAdminRecipeName(CbAdminRecipeName);
            BtEditRecipe.Enabled = false;
            BtDeleteRecipe.Enabled = false;
        }

        private void BtDeleteRecipe_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.DelRecipe_Admin(RecipeID);
            ldb.GetDataToGrdRecipeList(GrdRecipeList);
            ldb.GetDataToGrdIngredients(GrdIngredients);
            ldb.GetDatatoCbAdminCategoryName(CbAdminCategoryName);
            ldb.GetDatatoCbAdminRecipeName(CbAdminRecipeName);
            BtEditRecipe.Enabled = false;
            BtDeleteRecipe.Enabled = false;
        }

        private void btRefAllData_Click(object sender, EventArgs e)
        {
            DBStartUp();
        }

        //-----------------------------------------------ADMIN_INGREDIENTS------------------------------------------------------

        private void CbAdminRecipeName_SelectionChangeCommitted(object sender, EventArgs e)
        {

            CbAdminSelectIngre.Enabled = true;
            RecipeID_Ing = Convert.ToInt32(CbAdminRecipeName.SelectedValue);
            LoadDB Ldb = new LoadDB();
            Ldb.GetDatatoCbAdminSelectIngre(CbAdminSelectIngre, RecipeID_Ing);
            Ldb.LoadGrdIngredientsInRecipe(GrdIngredients, RecipeID_Ing);

            RdAdminSelectIngre.Checked = true;
        }

        private void btAddIngre_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();
            RecipeID = Convert.ToInt32(CbAdminRecipeName.SelectedValue);

            ex.AddIngredient_Admin(Convert.ToInt32(CbAdminRecipeName.SelectedValue), RdAdminSelectIngre.Checked, CbAdminSelectIngre.Text.Trim(), RdAdminTypeIngre.Checked, txtAdminTypeIngre.Text.Trim(), Convert.ToInt32(numAdminNetIngre.Value), txtAdminUnitIngre.Text.Trim());
            ldb.GetDatatoCbAdminSelectIngre(CbAdminSelectIngre, RecipeID_Ing);
            ldb.LoadGrdIngredientsInRecipe(GrdIngredients, RecipeID_Ing);
        }

        private void GrdIngredients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LoadDB ldb = new LoadDB();

                RecipeID_Ing = Convert.ToInt32(GrdIngredients.CurrentRow.Cells["RecipeID"].Value);
                CbAdminRecipeName.SelectedValue = Convert.ToInt32(GrdIngredients.CurrentRow.Cells["RecipeID"].Value);
                CbAdminRecipeName.Text = ldb.GetRecipetoCb_IngAdmin(RecipeID_Ing);
                txtAdminTypeIngre.Text = GrdIngredients.CurrentRow.Cells["IngredientName"].Value.ToString();
                numAdminNetIngre.Value = Convert.ToInt32(GrdIngredients.CurrentRow.Cells["Quantity"].Value);
                txtAdminUnitIngre.Text = GrdIngredients.CurrentRow.Cells["Unit"].Value.ToString();
                RdAdminTypeIngre.Checked = true;

                btEditIngre.Enabled = true;
                btDeleteIngre.Enabled = true;
            }
            catch
            {
                MessageBox.Show("No data in blank row!");
            }
        }

        private void btEditIngre_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.UpdateIngre_Admin(Convert.ToInt32(CbAdminRecipeName.SelectedValue), txtAdminTypeIngre.Text.Trim(), Convert.ToInt32(numAdminNetIngre.Value), txtAdminUnitIngre.Text.Trim());
            ldb.GetDatatoCbAdminSelectIngre(CbAdminSelectIngre, RecipeID_Ing);
            ldb.GetDataToGrdIngredients(GrdIngredients);

            btEditIngre.Enabled = false;
            btDeleteIngre.Enabled = false;
        }

        private void btDeleteIngre_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.DelIngre_Admin(Convert.ToInt32(CbAdminRecipeName.SelectedValue), txtAdminTypeIngre.Text.Trim());
            ldb.GetDatatoCbAdminSelectIngre(CbAdminSelectIngre, RecipeID_Ing);
            ldb.GetDataToGrdIngredients(GrdIngredients);

            btEditIngre.Enabled = false;
            btDeleteIngre.Enabled = false;
        }

        //-----------------------------------------------ADMIN_RESEEDID------------------------------------------------------


        private void BtReseedSupplyItemID_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            ex.ReseedID("SupplyItems");
        }

        private void BtReseedRecipeID_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            ex.ReseedID("Recipe");
        }

        private void BtReseedCategoryID_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            ex.ReseedID("RecipeCategory");
        }

        //-----------------------------------------------REGISTER SHOPPING LIST------------------------------------------------------

        private void BtRegisterShoppingList_Click(object sender, EventArgs e)
        {
            ExcuDB ex = new ExcuDB();
            LoadDB ldb = new LoadDB();

            ex.RegisterList();
            ldb.GetDataToGrdSupplyItems(GrdSupItems);
            ldb.GetDataToGrdAccounting(GrdAccounting);
        }

        private void cbSelectLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtInsertIngrLocation.Text = cbSelectLocation.Text;
            txtUpdateIngLocation.Text = cbSelectLocation.Text;
        }

        private void BtClearAcc_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you wan to Delete Accounting table?", "Delete Accounting table!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ConnectionDB con1 = new ConnectionDB();
                con1.Open();
                string st = "Delete from Accounting";
                con1.ExecuteNonQuery(st);
                con1.Close();
            }
        }

        private void BtClearLocation_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you wan to Delete Location table?", "Delete Location table!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ConnectionDB con1 = new ConnectionDB();
                con1.Open();
                string st = "Delete from Location";
                con1.ExecuteNonQuery(st);
                con1.Close();
            }
        }

        private void cbInsertIngCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUpdateIngCategory.Text = cbInsertIngCategory.Text;
            radioSelectIngCategory.Checked = true;
        }

        private void txtInsertIngName_MouseClick(object sender, MouseEventArgs e)
        {
            radioTypeIngName.Checked = true;
        }

        private void cbInsertIngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioSelectIngName.Checked = true;
        }

        private void txtInsertIngCategory_MouseClick(object sender, MouseEventArgs e)
        {
            radioTypeIngCategory.Checked = true;
        }

        private void CbAdminSelectIngre_SelectedIndexChanged(object sender, EventArgs e)
        {
            RdAdminSelectIngre.Checked = true;
        }

        private void txtAdminTypeIngre_MouseClick(object sender, MouseEventArgs e)
        {
            RdAdminTypeIngre.Checked = true;
        }

        private void btClearSupplyItems_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you wan to Delete SupplyItems table?", "Delete SupplyItems table!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ConnectionDB con1 = new ConnectionDB();
                con1.Open();
                string st = "Delete from SupplyItems";
                con1.ExecuteNonQuery(st);
                con1.Close();
            }
        }
    }
}
