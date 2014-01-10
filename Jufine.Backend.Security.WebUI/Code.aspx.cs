using System;
using Com.BaseLibrary.Web;

namespace Jufine.Backend.Security.WebUI
{
    public partial class Code : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationCodeUtility code = new ValidationCodeUtility();
            code.CodeSerial= "2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";

            code.GenerateGoogleStyleValidateCodeImage("VCode");
        }
    }
}