public class UserDataContainer : ISaveable
{
    private readonly UserData data;

    public UserDataContainer(UserData data)
    {
        this.data = data;

        InitializeData();
    }

    public object SaveAble => data;

    private void InitializeData()
    {
    }
}