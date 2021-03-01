package crc6491ce21829e59660c;


public class jadragentAccessibilityService
	extends android.accessibilityservice.AccessibilityService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAccessibilityEvent:(Landroid/view/accessibility/AccessibilityEvent;)V:GetOnAccessibilityEvent_Landroid_view_accessibility_AccessibilityEvent_Handler\n" +
			"n_onInterrupt:()V:GetOnInterruptHandler\n" +
			"n_onServiceConnected:()V:GetOnServiceConnectedHandler\n" +
			"";
		mono.android.Runtime.register ("jadr_agent.jadragentAccessibilityService, jadr-agent", jadragentAccessibilityService.class, __md_methods);
	}


	public jadragentAccessibilityService ()
	{
		super ();
		if (getClass () == jadragentAccessibilityService.class)
			mono.android.TypeManager.Activate ("jadr_agent.jadragentAccessibilityService, jadr-agent", "", this, new java.lang.Object[] {  });
	}


	public void onAccessibilityEvent (android.view.accessibility.AccessibilityEvent p0)
	{
		n_onAccessibilityEvent (p0);
	}

	private native void n_onAccessibilityEvent (android.view.accessibility.AccessibilityEvent p0);


	public void onInterrupt ()
	{
		n_onInterrupt ();
	}

	private native void n_onInterrupt ();


	public void onServiceConnected ()
	{
		n_onServiceConnected ();
	}

	private native void n_onServiceConnected ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
