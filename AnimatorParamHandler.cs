using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimatorParamHandler : StateMachineBehaviour {
	[Serializable]
	public struct ctrlInfo {
		public enum valueType { Float = 0, Int = 1, Bool = 2, Trigger = 3 };
		public enum opType { Add, Set, Off, Flip };
		public valueType type;
		public opType toDo;
		public string name;
		public float floatData;
		public int intData;
		public override string ToString() {
			return type.ToString() + ":" + toDo.ToString() + ":" + name;
		}
	}
	[SerializeField]
	public List<ctrlInfo> Enter = new List<ctrlInfo>();
	[SerializeField]
	public List<ctrlInfo> Leave = new List<ctrlInfo>();

	private bool process(ctrlInfo I, Animator A) {
		switch(I.type) {
		case ctrlInfo.valueType.Bool:
			switch(I.toDo) {
			case ctrlInfo.opType.Flip:
				A.SetBool(I.name, !A.GetBool(I.name));
				return true;
			case ctrlInfo.opType.Off:
				A.SetBool(I.name, false);
				return true;
			case ctrlInfo.opType.Set:
				A.SetBool(I.name, true);
				return true;
			default:
				return false;
			}
		case ctrlInfo.valueType.Trigger:
			switch(I.toDo) {
			case ctrlInfo.opType.Set:
				A.SetTrigger(I.name);
				return true;
			case ctrlInfo.opType.Off:
				A.ResetTrigger(I.name);
				return true;
			default:
				return false;
			}
		case ctrlInfo.valueType.Float:
			switch(I.toDo) {
			case ctrlInfo.opType.Add:
				A.SetFloat(I.name, A.GetFloat(I.name) + I.floatData);
				return true;
			case ctrlInfo.opType.Set:
				A.SetFloat(I.name, I.floatData);
				return true;
			default:
				return false;
			}
		case ctrlInfo.valueType.Int:
			switch(I.toDo) {
			case ctrlInfo.opType.Add:
				A.SetInteger(I.name, A.GetInteger(I.name) + I.intData);
				return true;
			case ctrlInfo.opType.Set:
				A.SetInteger(I.name, I.intData);
				return true;
			default:
				return false;
			}
		default:
			return false;
		}
	}
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		foreach(var I in Enter) {
			if (!process(I, animator)) {
				Debug.LogWarning(I.ToString());
			}
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		foreach(var I in Leave) {
			if(!process(I, animator)) {
				Debug.LogWarning(I.ToString());
			}
		}
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
