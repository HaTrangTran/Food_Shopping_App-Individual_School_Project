using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace ShoppingApp
{
    class LoadDB
    {
        ConnectionDB con = new ConnectionDB();
        
        //------------Load DB to Combobox CbRecipe trong Tab Create New Plan Meal-------------------------
        public void GetDatatoCbRecipe(ComboBox MyComb, int MyID)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select RecipeID, RecipeName from Recipe where CategoryID = " + MyID;
                ds = con.ExecuteDataSet(sql);
                MyComb.DataSource = ds.Tables[0];
                MyComb.DisplayMember = "RecipeName";
                MyComb.ValueMember = "RecipeID";
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        //------------Load DB to Combobox CbRecipeCategory trong Tab Create New Plan Meal-------------------------
        public void GetDatatoCbRecipeCategory(ComboBox MyComb)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select * from RecipeCategory";
                ds = con.ExecuteDataSet(sql);
                MyComb.DataSource = ds.Tables[0];
                MyComb.DisplayMember = "CategoryName";
                MyComb.ValueMember = "CategoryID";
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }
        // ------------Load Data to GrdPlanMeal-------------
        public void GetDataToGrdPlanMeal(DataGridView MyGrid)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select RecipeName, PlannedPortion, UsedPortion from PlannedMeal Order by RecipeName";
                ds = con.ExecuteDataSet(sql);
                MyGrid.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        // ------------Load Data to GrdShoppingList-------------
        public void GetDataToGrdShoppingList(DataGridView MyGrid)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select * from ShoppingList Order by IngredientName";
                ds = con.ExecuteDataSet(sql);
                MyGrid.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }
        //------------Load DB to Combobox IngredientName IN Tab Supply Items-------------------------
        public void GetDatatoCombIngName(ComboBox MyComb)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select Distinct IngredientName from SupplyItems Order by IngredientName";
                ds = con.ExecuteDataSet(sql);
                MyComb.DataSource = ds.Tables[0];
                MyComb.DisplayMember = "IngredientName";
                MyComb.ValueMember = "IngredientName";
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        //------------Load DB to Combobox CategorytName IN Tab Supply Items-------------------------
        public void GetDatatoCombCategoryName(ComboBox MyComb)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select Distinct IngreCategory from SupplyItems where IngreCategory is not null Order by IngreCategory"; ;
                ds = con.ExecuteDataSet(sql);
                MyComb.DataSource = ds.Tables[0];
                MyComb.DisplayMember = "IngreCategory";
                MyComb.ValueMember = "IngreCategory";
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }


        //------------Load DB to GriSupItems IN Tab Supply Items-----------
        public void GetDataToGrdSupplyItems(DataGridView MyGrid)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select SI.IngredientID, SI.IngredientName, SI.Quantity,SI.Unit, SI.DateOfArrival,SI.ExpireDate,SI.Price,SI.IngreCategory, LC.LocationName from SupplyItems SI INNER JOIN Location LC ON SI.IngredientID = LC.IngredientID where SI.Quantity >0 Order by SI.DateOfArrival DESC, SI.IngredientID ASC";
                ds = con.ExecuteDataSet(sql);
                MyGrid.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        //---------------Load DB to Listbox RecipeName IN Tab Portion Confirm-------------------
        public void GetDatatoLstRecipeName(ListBox MyListbox)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select RecipeID, RecipeName from PlannedMeal Order by RecipeName";
                ds = con.ExecuteDataSet(sql);
                MyListbox.DataSource = ds.Tables[0];
                MyListbox.DisplayMember = "RecipeName";
                MyListbox.ValueMember = "RecipeID";

                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        // ------------Load DB to GrdAccounting in Tab Counting-------------
        public void GetDataToGrdAccounting(DataGridView MyGrid)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select * from Accounting Order by DateOfArrival DESC, IngredientName ASC";
                ds = con.ExecuteDataSet(sql);
                MyGrid.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        // ------------Load DB to Load DB to GrdRecipeCategory IN Tab Admin/Category-------------
        public void GetDataToGrdRecipeCategory(DataGridView MyGrid)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select * from RecipeCategory Order by CategoryID";
                ds = con.ExecuteDataSet(sql);
                MyGrid.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }
        // ------------Load DB to Load DB to GrdRecipeList IN Tab Admin/Category-------------
        public void GetDataToGrdRecipeList(DataGridView MyGrid)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select * from Recipe Order by RecipeID";
                ds = con.ExecuteDataSet(sql);
                MyGrid.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }
        //------------Load DB to Combobox CbAdminCategoryName IN Tab Tab Admin/Recipe-------------------------
        public void GetDatatoCbAdminCategoryName(ComboBox MyComb)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select * from RecipeCategory Order by CategoryName";
                ds = con.ExecuteDataSet(sql);
                MyComb.DataSource = ds.Tables[0];
                MyComb.DisplayMember = "CategoryName";
                MyComb.ValueMember = "CategoryID";
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        // ------------Load DB to Load DB to GrdIngredients IN Tab Admin/Ingredients-------------
        public void GetDataToGrdIngredients(DataGridView MyGrid)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select * from Ingredients Order by RecipeID";
                ds = con.ExecuteDataSet(sql);
                MyGrid.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        // ------------Load DB to Load DB to GrdIngredients in Tab Admin/Ingredients (AFTER SELECT RECIPE)-------------
        public void LoadGrdIngredientsInRecipe(DataGridView MyGrid, int RecID)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select * from Ingredients WHERE RecipeID =" + RecID;
                ds = con.ExecuteDataSet(sql);
                MyGrid.DataSource = ds.Tables[0];
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        //------------Load DB to CbAdminRecipeName IN Tab Admin/Ingredients-------------------------
        public void GetDatatoCbAdminRecipeName(ComboBox MyComb)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "select RecipeID, RecipeName from Recipe Order by RecipeName";
                ds = con.ExecuteDataSet(sql);
                MyComb.DataSource = ds.Tables[0];
                MyComb.DisplayMember = "RecipeName";
                MyComb.ValueMember = "RecipeID";
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        //------------Load DB to CbAdminSelectIngreName IN Tab Admin/Ingredients-------------------------
        public void GetDatatoCbAdminSelectIngre(ComboBox MyComb, int MyID)
        {
            try
            {
                //con.Open();
                //DataSet ds = new DataSet();
                //string sql = "select Distinct IngredientName from SupplyItems Order by IngredientName";
                //ds = con.ExecuteDataSet(sql);
                //MyComb.DataSource = ds.Tables[0];
                //MyComb.DisplayMember = "IngredientName";
                //MyComb.ValueMember = "IngredientName";
                //con.Close();

                con.Open();
                DataSet ds = new DataSet();
                string sql = "SELECT IngredientName FROM Ingredients WHERE RecipeID =" + MyID;
                ds = con.ExecuteDataSet(sql);
                MyComb.DataSource = ds.Tables[0];
                MyComb.DisplayMember = "IngredientName";
                MyComb.ValueMember = "IngredientName";
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        
        public string GetLocationName(int IngID)
        {
            string str = "Select LocationName from Location where IngredientID =" + IngID;
            ConnectionDB connect = new ConnectionDB();
            con.Open();
            SqlDataReader R;
            R = con.ExecuteReader(str);
            string st = null;
            if (R.HasRows)
            {
                while (R.Read())
                {
                    st = R["LocationName"].ToString();
                }
                return st;
            }
            else
            {
                return null;
            }
            con.Close();
        }

        //------------------------------------------------------------------------------------------

        public string GetCateNametoCb_RecAdmin(int CateID)
        {
            string CateName = null;
            con.Open();
            SqlDataReader Rd;
            string sql = "SELECT CategoryName FROM RecipeCategory WHERE CategoryID = " + CateID;
            Rd = con.ExecuteReader(sql);
            if (Rd.HasRows)
            {
                while (Rd.Read())
                {
                    CateName = Rd["CategoryName"].ToString();
                }
                return CateName;
            }
            else
            {
                return null;
            }
            con.Close();
        }

        public string GetRecipetoCb_IngAdmin(int RecID)
        {
            string RecName = null;
            con.Open();
            SqlDataReader Rd;
            string sql = "SELECT RecipeName FROM Recipe WHERE RecipeID = " + RecID;
            Rd = con.ExecuteReader(sql);
            if (Rd.HasRows)
            {
                while (Rd.Read())
                {
                    RecName = Rd["RecipeName"].ToString();
                }
                return RecName;
            }
            else
            {
                return null;
            }
            con.Close();
        }

        //------------Load DB to Location Combobox in SupplyItems-------------------------
        public void GetDatatoCbLocation(ComboBox MyComb)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                string sql = "SELECT Distinct LocationName FROM Location WHERE LocationName is not null ORDER BY LocationName";
                ds = con.ExecuteDataSet(sql);
                MyComb.DataSource = ds.Tables[0];
                MyComb.DisplayMember = "LocationName";
                MyComb.ValueMember = "LocationName";
                con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
    }

}

