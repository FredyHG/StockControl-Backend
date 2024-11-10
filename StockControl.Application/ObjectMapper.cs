namespace StockControl.Application;

public class ObjectMapper
{
    public static void CopyNonNull(object origin, object target)
    {
        var originType = origin.GetType();
        var targetType = target.GetType();

        foreach (var propOrigin in originType.GetProperties())
        {
            var valueOrigin = propOrigin.GetValue(origin, null);
            if (valueOrigin != null)
            {
                var propTarget = targetType.GetProperty(propOrigin.Name);
                if (propTarget != null && propTarget.CanWrite) propTarget.SetValue(target, valueOrigin, null);
            }
        }
    }
}