﻿using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.FFXIV.Client.UI.Agent; 

[Agent(AgentId.Salvage)]
[StructLayout(LayoutKind.Explicit, Size = 0x3D0)]
public unsafe partial struct AgentSalvage {
    public static AgentSalvage* Instance() => Framework.Instance()->GetUiModule()->GetAgentModule()->GetAgentSalvage();
    
    [FieldOffset(0x00)] public AgentInterface AgentInterface;

    [FieldOffset(0x30)] public SalvageItemCategory SelectedCategory;
    [FieldOffset(0x38)] public void* ItemList; // Maybe?
    [FieldOffset(0x40)] public Utf8String TextCarpenter;
    [FieldOffset(0xA8)] public Utf8String TextBlacksmith;
    [FieldOffset(0x110)] public Utf8String TextArmourer;
    [FieldOffset(0x178)] public Utf8String TextGoldsmith;
    [FieldOffset(0x1E0)] public Utf8String TextLeatherworker;
    [FieldOffset(0x248)] public Utf8String TextWeaver;
    [FieldOffset(0x2B0)] public Utf8String TextAlchemist;
    [FieldOffset(0x318)] public Utf8String TextCulinarian;
    [FieldOffset(0x380)] public uint ItemCount;

    [FieldOffset(0x398)] public InventoryItem* DesynthItemSlot;

    [FieldOffset(0x3B0)] public SalvageResult DesynthItem;
    [FieldOffset(0x3BC)] public uint DesynthItemId;
    
    [FixedArray(typeof(SalvageResult), 3)]
    [FieldOffset(0x3C0)] public fixed byte DesynthResult[8 * 3];

    public Span<SalvageResult> DesynthResultSpan {
        get {
            fixed (byte* ptr = DesynthResult)
                return new Span<SalvageResult>(ptr, 3);
        }
    }

    [MemberFunction("E8 ?? ?? ?? ?? 48 8B 7C 24 ?? C7 85")]
    public partial void* ItemListRefresh();
    
    [MemberFunction("E8 ?? ?? ?? ?? 41 81 BD ?? ?? ?? ?? ?? ?? ?? ?? 7D 1A")]
    public partial void* ItemListAdd(bool meetsLevelRequirement, InventoryType containerId, int containerSlot, uint itemId, void* exdRow, uint quantity);
    
    public enum SalvageItemCategory {
        InventoryEquipment,
        InventoryHousing,
        ArmouryMainOff,
        ArmouryHeadBodyHands,
        ArmouryLegsFeet,
        ArmouryNeckEars,
        ArmouryWristsRings,
        Equipped,
    }
}

[StructLayout(LayoutKind.Explicit, Size = 8)]
public struct SalvageResult {
    [FieldOffset(0x00)] public uint ItemId;
    [FieldOffset(0x04)] public int Quantity;
}