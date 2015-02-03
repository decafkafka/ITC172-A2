using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    ShowTrackerEntities showentities = new ShowTrackerEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var artists = from a in showentities.Artists
                          orderby a.ArtistName
                          select new { a.ArtistName, a.ArtistKey };

            DropDownList1.DataSource = artists.ToList();
            DropDownList1.DataTextField = "ArtistName";
            DropDownList1.DataValueField = "ArtistKey";
            DropDownList1.DataBind();
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int key = int.Parse(DropDownList1.SelectedValue.ToString());
        var showList = from s in showentities.Shows
                    from sd in s.ShowDetails
                    where sd.ArtistKey == key
                    select new {sd.Artist.ArtistName,
                        sd.Artist.ArtistWebPage,
                        s.ShowName,
                        s.ShowDate,
                        s.ShowTime,
                        sd.ShowDetailArtistStartTime,
                        s.ShowTicketInfo,
                        sd.ShowDetailAdditional};
        GridView1.DataSource = showList.ToList();
        GridView1.DataBind();
    }
}