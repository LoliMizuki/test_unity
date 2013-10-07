using UnityEngine;
using System.Collections;

public class TestLogAssert : MonoBehaviour
{
	public enum TestType
	{
		None,
		Log,
		AssertIfNull,
		Assert,
		AssertFalse,
	}

	public TestType RequireTest = TestType.None;

	void Start()
	{
		if( RequireTest != TestType.None )
			TestAsserts();
		else
			ChainCalls();
	}

	void Update()
	{

	}

	void TestAsserts()
	{
		switch( RequireTest )
		{
			case TestType.Log:
				MZDebug.Log( "Log Here" );
				break;

			case TestType.AssertIfNull:
				MZDebug.AssertIfNull( new GameObject() ); // this line not output
				GameObject nullObject = null;
				MZDebug.AssertIfNull( nullObject );
				break;

			case TestType.Assert:
				MZDebug.Assert( false, "False Statements" );
				break;

			case TestType.AssertFalse:
				MZDebug.AssertAlwaysFalse( "Always False" );
				break;
		}
	}

	void ChainCalls()
	{
//		MZDebug.Log( "I am in ChainCalls()?" );
//		MZDebug.Trace( 2 );
//		MZDebug.Trace();

		A();
	}

	void A()
	{
//		MZDebug.Log( "I am in A()?" );
		B();
	}

	void B()
	{
		C();
	}

	void C()
	{
		D();
	}

	void D()
	{
//		MZDebug.Log( "--- Trace Test ---" );
//		MZDebug.Log( "I am in D()?" );
//		for( int i = 0; i < 4; i++ )
//			MZDebug.Trace( i );

//		MZDebug.Log( "Below will show from D()" );
//
//		string s1 = "string1";
//		string s2 = "string2";
//		MZDebug.Log( "{0} => {1}", s1, s2 );
//
//		MZDebug.LogBreak( "Break here" );
//		MZDebug.Alert( true, "Alert here" );
	}
}
