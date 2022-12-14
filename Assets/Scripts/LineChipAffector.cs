using GGMatch3;
using System.Collections.Generic;
using UnityEngine;

public class LineChipAffector : ChipAffectorBase
{
	public List<Slot> affectedSlots = new List<Slot>();

	public List<LightingBolt> bolts = new List<LightingBolt>();

	private float affectorDuration;

	private IntVector2 direction;

	private Slot originSlot;

	private float angle;

	private PowerLineAffector.Settings settings => Match3Settings.instance.powerLineAffectorSettings;

	public override void AddToInputAffectorExport(Match3Game.InputAffectorExport inputAffector)
	{
		if (originSlot != null)
		{
			Match3Game.InputAffectorExport.InputAffectorForSlot inputAffectorForSlot = new Match3Game.InputAffectorExport.InputAffectorForSlot();
			inputAffectorForSlot.bolts.AddRange(bolts);
			bolts.Clear();
			inputAffectorForSlot.slot = originSlot;
			inputAffector.affectorExports.Add(inputAffectorForSlot);
		}
	}

	public override void Clear()
	{
		affectorDuration = 0f;
		for (int i = 0; i < bolts.Count; i++)
		{
			bolts[i].RemoveFromGame();
		}
		bolts.Clear();
		lockContainer.UnlockAll();
	}

	public void Init(Slot originSlot, Match3Game game, IntVector2 direction)
	{
		Clear();
		this.originSlot = originSlot;
		this.direction = direction;
		base.globalLock.isSlotGravitySuspended = true;
		base.globalLock.isChipGeneratorSuspended = true;
		IntVector2 position = originSlot.position;
		Slot[] slots = game.board.slots;
		foreach (Slot slot in slots)
		{
			if (slot == null)
			{
				continue;
			}
			IntVector2 intVector = slot.position - position;
			float num = 0f;
			if (this.direction.y != 0)
			{
				Mathf.Abs(intVector.y);
				num = Mathf.Abs(intVector.x);
			}
			else
			{
				Mathf.Abs(intVector.x);
				num = Mathf.Abs(intVector.y);
			}
			if (!(num > 0f))
			{
				base.globalLock.LockSlot(slot);
				affectedSlots.Add(slot);
				if (!Match3Settings.instance.playerInputSettings.disableLightingInAffectors && !Match3Settings.instance.playerInputSettings.useSimpleLineBolts)
				{
					LightingBolt lightingBolt = game.CreateLightingBoltPowerup();
					lightingBolt.Init(originSlot, slot);
					lightingBolt.SetEndPositionFromLerp(0f);
					bolts.Add(lightingBolt);
				}
			}
		}
		if (Match3Settings.instance.playerInputSettings.useSimpleLineBolts)
		{
			Slot slot2 = game.LastSlotOnDirection(originSlot, direction);
			Slot slot3 = game.LastSlotOnDirection(originSlot, -direction);
			if (slot2 != null && slot2 != originSlot)
			{
				LightingBolt lightingBolt2 = game.CreateLightingBoltPowerup();
				lightingBolt2.Init(originSlot, slot2);
				lightingBolt2.SetEndPositionFromLerp(0f);
				bolts.Add(lightingBolt2);
			}
			if (slot3 != null && slot3 != originSlot)
			{
				LightingBolt lightingBolt3 = game.CreateLightingBoltPowerup();
				lightingBolt3.Init(originSlot, slot3);
				lightingBolt3.SetEndPositionFromLerp(0f);
				bolts.Add(lightingBolt3);
			}
		}
	}

	public override void Update()
	{
		angle += Time.deltaTime * settings.angleSpeed;
		affectorDuration += Time.deltaTime;
		float endPositionFromLerp = Mathf.InverseLerp(0f, settings.outDuration, affectorDuration);
		for (int i = 0; i < bolts.Count; i++)
		{
			bolts[i].SetEndPositionFromLerp(endPositionFromLerp);
		}
		for (int j = 0; j < affectedSlots.Count; j++)
		{
			Slot slot = affectedSlots[j];
			ApplyShake(slot, originSlot.localPositionOfCenter);
		}
	}

	private void ApplyShake(Slot slot, Vector3 startLocalPosition)
	{
		Vector3 lhs = slot.localPositionOfCenter - startLocalPosition;
		if (lhs == Vector3.zero)
		{
			lhs = Vector3.right;
		}
		lhs.z = 0f;
		float d = Mathf.Sin((settings.phaseOffsetMult * lhs.sqrMagnitude + angle) * 57.29578f) * settings.amplitude;
		Vector3 vector = lhs.normalized * d;
		slot.offsetPosition += vector;
	}
}
