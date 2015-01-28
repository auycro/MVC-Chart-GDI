<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Graph Content</h3>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="GraphContent" runat="server">
    <table>
        <tr>
            <td>Pie Chart<p></p><img src="/Veggie/PieChart" /></td>
            <td>Bar Chart<p></p><img src="/Veggie/BarChart" /></td>
        </tr>
    </table>
</asp:Content>