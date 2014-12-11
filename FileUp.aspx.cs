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
using System.IO;
public partial class FileUp : System.Web.UI.Page
{
    //调用公共类
    CommonClass CC = new CommonClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SaveFUC();
        }
    }
    /// <summary>
    /// 用于保存当前页面上传文件控件集到缓存中
    /// </summary>
    protected void SaveFUC()
    { 
        //创建动态增加数组
        ArrayList AL = new ArrayList();
        foreach (Control C in tabFU.Controls)
        {
            if (C.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlTableRow")
            {
                HtmlTableCell HTC = (HtmlTableCell)C.Controls[0];
                foreach (Control FUC in HTC.Controls)
                {
                    //判断该控件是否为上传控件（FileUpLoad），如果是，则添加到ArrayList中
                    if (FUC.GetType().ToString() == "System.Web.UI.WebControls.FileUpload")
                    {
                        FileUpload FU = (FileUpload)FUC;
                        AL.Add(FU);
                    }
                }
            }
        }
        //将保存在数组ArrayList中的所有上传控件（FileUpLoad），添加到缓存中，命名为“FilesControls”
        Session.Add("FilesControls",AL); 
    }
    /// <summary>
    /// 用于执行添加一个上传文件控件的操作
    /// </summary>
    protected void InsertFUC()
    {
        ArrayList AL = new ArrayList();
        //清空表格tabFU中原有的上传控件
        this.tabFU.Rows.Clear();
        //调用GetFUCInfo方法，将存放在缓存中的上传控件添加到表格tabFU中
        GetFUCInfo();
        //在表格tabFU中添加一个上传控件
        HtmlTableRow HTR = new HtmlTableRow();
        HtmlTableCell HTC = new HtmlTableCell();
        HTC.Controls.Add(new FileUpload());
        HTR.Controls.Add(HTC);
        tabFU.Rows.Add(HTR);
        //调用SaveFUC方法，将添加的上传控件保存在缓存中
        SaveFUC();
    } 
    /// <summary>
    /// 用于读取缓存中存储的上传文件控件集
    /// </summary>
    protected void GetFUCInfo()
    {
        ArrayList AL = new ArrayList();
        //判断缓存中是否已存在上传控件
        if (Session["FilesControls"] != null)
        {
            //将缓存中的上传控件集存放在数据集ArrayList中
            AL = (System.Collections.ArrayList)Session["FilesControls"];
            for (int i = 0; i < AL.Count; i++)
            {
                HtmlTableRow HTR = new HtmlTableRow();
                HtmlTableCell HTC = new HtmlTableCell();
                HTC.Controls.Add((System.Web.UI.WebControls.FileUpload)AL[i]);
                HTR.Controls.Add(HTC);
                //将上传控件添加到名为tabFU表格中
                tabFU.Rows.Add(HTR);
            }
        }
    }
   
    /// <summary>
    /// 用于执行文件上传操作
    /// </summary>
    //文件是否上传（1：上传成功，0：文件未被上传）
    public static int IntIsUF = 0;
    private void UpFile()
    {   
        //获取文件保存的路径
        string FilePath = Server.MapPath(".//") + "Files";
        //获取由客户端上载文件的控件集合
        HttpFileCollection HFC = Request.Files;
        for (int i = 0; i < HFC.Count; i++)
        {
            //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = HFC[i];
            try
            {
                if (UserHPF.ContentLength > 0)
                {
                    //调用GetAutoID方法获取上传文件自动编号
                    int IntFieldID =CC.GetAutoID("fileID", "tb_files");
                    //文件的真实名（格式：[文件编号]上传文件名）
                    //用于实现上传多个相同文件时，原有文件不被覆盖
                    string strFileTName = "[" + IntFieldID + "]" + System.IO.Path.GetFileName(UserHPF.FileName);
                    //定义插入字符串，将上传文件信息保存在数据库中
                    string sqlStr = "insert into tb_files(fileID,fileName,fileLoad,fileUpDate,fileTrueName)";
                    sqlStr += "values('" +IntFieldID + "'";
                    sqlStr += ",'" + System.IO.Path.GetFileName(UserHPF.FileName) + "'";
                    sqlStr += ",'" + FilePath + "'";
                    sqlStr += ",'" + DateTime.Now.ToLongDateString() + "'";
                    sqlStr += ",'" + strFileTName + "')";
                    //打开与数据库的连接
                    SqlConnection myConn = CC.GetConnection();
                    myConn.Open();
                    SqlCommand myCmd = new SqlCommand(sqlStr, myConn);
                    myCmd.ExecuteNonQuery();
                    myCmd.Dispose();
                    myConn.Dispose();
                    //将上传的文件存放在指定的文件夹中
                    UserHPF.SaveAs(FilePath + "//" + strFileTName);
                    IntIsUF = 1;
                }  
            }
            catch
            {
                //文件上传失败，清空缓存中的上传控件集，重新加载上传页面
                if (Session["FilesControls"] != null)
                {
                    Session.Remove("FilesControls");
                }
                Response.Write(CC.MessageBox("处理出错！", "FileUp.aspx"));
                return;
            }
           
        }
        //当文件上传成功或者没有上传文件，都需要清空缓存中的上传控件集，重新加载上传页面
        if (Session["FilesControls"] != null)
        {
            Session.Remove("FilesControls");
        }
        if (IntIsUF == 1)
        {
            IntIsUF = 0;
            Response.Write(CC.MessageBox("上传成功！", "FileUp.aspx"));
        }
        else
        {
            Response.Write(CC.MessageBox("请选择上传文件！", "FileUp.aspx"));   
        }       
    }
    protected void btnAddFU_Click(object sender, EventArgs e)
    {
        //执行添加上传控件方法
        InsertFUC();
    }
    protected void btnUp_Click(object sender, EventArgs e)
    {
        //执行上传文件
        UpFile();
    }
   
}
