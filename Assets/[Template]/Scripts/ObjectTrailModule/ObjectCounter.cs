using TMPro;

namespace Template
{
    public class ObjectCounter : BaseMono
    {
        public TextMeshProUGUI txtCount;

        public void UpdateCounter(int wheelCount)
        {
            txtCount.text = wheelCount.ToString();
        }
    }
}