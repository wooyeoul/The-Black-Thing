using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IPlayerInterface
{
    public abstract int GetChapter();
    public abstract LANGUAGE GetLanguage();
    public abstract void SetMoonRadioIdx(int Idx);
    public abstract int GetMoonRadioIdx();
}
