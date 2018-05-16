using Unity.Entities;


public class CalculatePosition {}

[UpdateAfter(typeof(CalculatePosition))]
public class UpdateTransform {}
