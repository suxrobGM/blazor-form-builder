namespace FormBuilder.Models;

public class TextField : Field<string>
{
    public override FieldType Type => FieldType.Text;
    
    public long? GetMaxLength()
    {
        return Validators.OfType<LengthValidator>().FirstOrDefault()?.MaxLength;
    }
}
