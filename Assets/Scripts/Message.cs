/// <summary>
///     Stores a text message and its <see cref="MessageType"/>
/// </summary>
public class Message
{
    /// <summary>
    ///     Gets or sets the text.
    /// </summary>
    /// <value>
    ///     The text.
    /// </value>
    public string Text { get; set; }

    /// <summary>
    ///     Gets or sets the type (or severity).
    /// </summary>
    /// <value>
    ///     The type.
    /// </value>
    public MessageType Type { get; set; }

    /// <summary>
    ///     Gets a value indicating whether this instance has text.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance has text; otherwise, <c>false</c>.
    /// </value>
    public bool HasText => string.IsNullOrWhiteSpace(this.Text);

    /// <summary>
    ///     Initializes a new instance of the <see cref="Message"/> class.
    /// </summary>
    public Message()
    {
        this.Text = "";
    }
}
