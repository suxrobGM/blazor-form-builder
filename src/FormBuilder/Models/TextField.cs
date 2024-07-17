namespace FormBuilder.Models;

public class TextField : Field<string>
{
    public TextField()
    {
        Type = FieldType.Text;
    }
}
