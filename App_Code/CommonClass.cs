using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// CommonClass 的摘要说明
/// </summary>
public class CommonClass
{
	public CommonClass()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 连接数据库
    /// </summary>
    /// <returns>返回SqlConnection对象</returns>
    public SqlConnection GetConnection()
    {
        string myStr = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        SqlConnection myConn = new SqlConnection(myStr);
        return myConn;
    }
    /// <summary>
    /// 说明：MessageBox用来在客户端弹出对话框。
    /// 参数：TxtMessage 对话框中显示的内容。
    /// 参数：Url 对话框关闭后，跳转的页
    /// </summary>
    public string MessageBox(string TxtMessage,string Url)
    {
        string str;
        str = "<script language=javascript>alert('" + TxtMessage + "');location='" + Url + "'</script>";
        return str;
    }
    /// <summary>
    /// 说明：MessageBox用来在客户端弹出对话框。
    /// 参数：TxtMessage 对话框中显示的内容。
    /// </summary>
    public string MessageBox(string TxtMessage)
    {
        string str;
        str = "<script language=javascript>alert('" + TxtMessage + "')</script>";
        return str;
    }
    /// <summary>
    /// 实现自动编号
    /// </summary>
    /// <param name="FieldName">自动编号的字段名</param>
    /// <param name="TableName">表名</param>
    /// <returns>返回编号</returns>
    public int GetAutoID(string FieldName, string TableName)
    {
        SqlConnection myConn =GetConnection();
        SqlCommand myCmd = new SqlCommand("select Max(" + FieldName + ") as MaxID from " + TableName, myConn);
        SqlDataAdapter dapt = new SqlDataAdapter(myCmd);
        DataSet ds = new DataSet();
        dapt.Fill(ds);
        if (ds.Tables[0].Rows[0][0].ToString() == "")
        {
            return 1;
        }
        else
        {
            int IntFieldID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) + 1;
            return (Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) + 1);
        }
    }
    ///// <summary>
    ///// 查看具有相同名字的文件数
    ///// </summary>
    ///// <param name="sqlStr">查询字符串</param>
    ///// <returns>返回相同文件数</returns>
    //public int GetRowCount(string sqlStr)
    //{
    //    //打开与数据库的连接
    //    SqlConnection myConn = CC.GetConnection();
    //    myConn.Open();
    //    SqlCommand myCmd = new SqlCommand(sqlStr, myConn);
    //    int IntRowCount = Convert.ToInt32(myCmd.ExecuteScalar().ToString());
    //    //释放资源
    //    myCmd.Dispose();
    //    myConn.Close();
    //    //返回行数
    //    return IntRowCount;
    //}
}
