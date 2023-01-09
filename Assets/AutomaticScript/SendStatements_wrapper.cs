using UnityEngine;
using FYFY;

public class SendStatements_wrapper : BaseWrapper
{
	private void Start()
	{
		this.hideFlags = HideFlags.NotEditable;
	}

	public void initGBLXAPI()
	{
		MainLoop.callAppropriateSystemMethod (system, "initGBLXAPI", null);
	}

	public void testSendStatement()
	{
		MainLoop.callAppropriateSystemMethod (system, "testSendStatement", null);
	}

	public void selectSkinSendStatement(System.Int32 i)
	{
		MainLoop.callAppropriateSystemMethod (system, "selectSkinSendStatement", i);
	}

	public void moneySendStatement()
	{
		MainLoop.callAppropriateSystemMethod (system, "moneySendStatement", null);
	}

}
