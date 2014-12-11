using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public partial class FileInfo : System.Web.UI.Page
{
    CommonClass CC = new CommonClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //打开数据库
            SqlConnection myConn = CC.GetConnection();
            myConn.Open();
            //从数据库中获取指定文件的数据信息
            string sqlStr = "select fileName,fileUpDate,fileLoad from tb_files where fileID=" + Convert.ToInt32(Request["id"].ToString());
            SqlDataAdapter dapt = new SqlDataAdapter(sqlStr, myConn);
            DataSet ds = new DataSet();
            dapt.Fill(ds, "files");
            if (ds.Tables["files"].Rows.Count > 0)
            {
                //显示文件的数据信息
                Response.Write("文件的相关信息");
                Response.Write("<hr>");
                Response.Write("文件所在位置：" + ds.Tables["files"].Rows[0][2].ToString() + "<br>");
                Response.Write("文件名：" + ds.Tables["files"].Rows[0][0].ToString() + "<br>");
                Response.Write("创建时间：" + ds.Tables["files"].Rows[0][1].ToString() + "<br>");
                Response.Write("<hr>");

            }
            myConn.Close();    
        }
    }
   
}
