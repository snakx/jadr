package mono.android.support.constraint.motion;


public class MotionLayout_TransitionListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.support.constraint.motion.MotionLayout.TransitionListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTransitionChange:(Landroid/support/constraint/motion/MotionLayout;IIF)V:GetOnTransitionChange_Landroid_support_constraint_motion_MotionLayout_IIFHandler:Android.Support.Constraints.Motion.MotionLayout/ITransitionListenerInvoker, Xamarin.Android.Support.Constraint.Layout\n" +
			"n_onTransitionCompleted:(Landroid/support/constraint/motion/MotionLayout;I)V:GetOnTransitionCompleted_Landroid_support_constraint_motion_MotionLayout_IHandler:Android.Support.Constraints.Motion.MotionLayout/ITransitionListenerInvoker, Xamarin.Android.Support.Constraint.Layout\n" +
			"n_onTransitionStarted:(Landroid/support/constraint/motion/MotionLayout;II)V:GetOnTransitionStarted_Landroid_support_constraint_motion_MotionLayout_IIHandler:Android.Support.Constraints.Motion.MotionLayout/ITransitionListenerInvoker, Xamarin.Android.Support.Constraint.Layout\n" +
			"n_onTransitionTrigger:(Landroid/support/constraint/motion/MotionLayout;IZF)V:GetOnTransitionTrigger_Landroid_support_constraint_motion_MotionLayout_IZFHandler:Android.Support.Constraints.Motion.MotionLayout/ITransitionListenerInvoker, Xamarin.Android.Support.Constraint.Layout\n" +
			"";
		mono.android.Runtime.register ("Android.Support.Constraints.Motion.MotionLayout+ITransitionListenerImplementor, Xamarin.Android.Support.Constraint.Layout", MotionLayout_TransitionListenerImplementor.class, __md_methods);
	}


	public MotionLayout_TransitionListenerImplementor ()
	{
		super ();
		if (getClass () == MotionLayout_TransitionListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Support.Constraints.Motion.MotionLayout+ITransitionListenerImplementor, Xamarin.Android.Support.Constraint.Layout", "", this, new java.lang.Object[] {  });
	}


	public void onTransitionChange (android.support.constraint.motion.MotionLayout p0, int p1, int p2, float p3)
	{
		n_onTransitionChange (p0, p1, p2, p3);
	}

	private native void n_onTransitionChange (android.support.constraint.motion.MotionLayout p0, int p1, int p2, float p3);


	public void onTransitionCompleted (android.support.constraint.motion.MotionLayout p0, int p1)
	{
		n_onTransitionCompleted (p0, p1);
	}

	private native void n_onTransitionCompleted (android.support.constraint.motion.MotionLayout p0, int p1);


	public void onTransitionStarted (android.support.constraint.motion.MotionLayout p0, int p1, int p2)
	{
		n_onTransitionStarted (p0, p1, p2);
	}

	private native void n_onTransitionStarted (android.support.constraint.motion.MotionLayout p0, int p1, int p2);


	public void onTransitionTrigger (android.support.constraint.motion.MotionLayout p0, int p1, boolean p2, float p3)
	{
		n_onTransitionTrigger (p0, p1, p2, p3);
	}

	private native void n_onTransitionTrigger (android.support.constraint.motion.MotionLayout p0, int p1, boolean p2, float p3);

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
