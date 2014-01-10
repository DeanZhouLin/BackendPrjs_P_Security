using Jufine.Backend.WebModel;


namespace Jufine.Backend.Security.WebUI
{
	public partial class Welcome1 : PageBase
	{
		public override bool IsNeedLogin
		{
			get
			{
				return false;
			}
		}
	}
}