using UnityEngine;

public class Dictionary : MonoBehaviour
{
    public string[] word4;
    public string[] word5;
    public string[] word6;
    public string[] word7;

    public string[,] words;

    public Dictionary()
    {
        words = new string[,]
        {
            {
                "save", "addy", "pear", "bene", "ogma", "mill", "yank", "geom", "shin", "pize",
                "lind", "ymha", "tour", "bure", "fear", "keek", "conj", "sant", "elli", "como",
                "lura", "sent", "gola", "raid", "pain", "dive", "cats", "save", "kure", "hewn",
                "buff", "nono", "leap", "sken", "bine", "gale", "reno", "evil", "horn", "circ",
                "wynn", "pleb", "sura", "alta", "peen", "bilk", "dunt", "kook", "raze", "luca", "coal", "rose"
            },
            {
                "huffy", "tonic", "folio", "dolph", "stool", "crony", "humor", "lords", "yodel", "gales",
                "faker", "alike", "bites", "kazan", "kirov", "sayer", "amber", "footy", "ludie", "rater",
                "hofer", "samia", "libri", "wigan", "sense", "klong", "swiss", "adnah", "doorn", "iyyar",
                "eager", "metol", "drank", "worth", "miser", "paget", "kyoga", "desde", "bayle", "gamma",
                "inure", "hades", "nomad", "ethyl", "biddy", "ether", "lumpy", "payne", "kyros", "dozer", "booze", "sakes"
            },
            {
                "employ", "shacko", "canula", "lesbos", "menial", "trashy", "banjul", "shrove", "scutch", "orange",
                "swiple", "gothic", "strong", "amatol", "kermes", "somnus", "sennar", "vilely", "ghibli", "orchil",
                "bailey", "maungy", "ginned", "flocci", "vernal", "immune", "bagnio", "briton", "aveiro", "cloete",
                "piquet", "febris", "betook", "colors", "coleus", "kendra", "aimful", "jansen", "tipple", "leaker",
                "couple", "diesel", "unhoed", "eduard", "teazle", "blunge", "oxygen", "prearm", "fowler", "tither", "merkel", "melody"
            },
            {
                "kristin", "testify", "moseley", "pompano", "kilobar", "whining", "alcaide", "cutover", "heizing", "myocoel",
                "incluse", "regroup", "georama", "ephesus", "felsite", "analogy", "locoman", "unopted", "outpush", "indoors",
                "slavkov", "strozza", "penaria", "airplot", "sheriff", "amyelia", "unlocal", "smoothy", "castoff", "cockily",
                "unicorn", "behoofs", "maureen", "coronis", "leporid", "tolbert", "peptise", "effulge", "retiree", "untrite",
                "absinth", "whangee", "ciliate", "herbert", "subduct", "unsling", "shingle", "lampion", "unmeted", "lockbox", "glimpse", "cameron"
            }
    };


        word4 = new string[]
        {
            "save", "addy", "pear", "bene", "ogma", "mill", "yank", "geom", "shin", "pize",
            "lind", "ymha", "tour", "bure", "fear", "keek", "conj", "sant", "elli", "como",
            "lura", "sent", "gola", "raid", "pain", "dive", "cats", "save", "kure", "hewn",
            "buff", "nono", "leap", "sken", "bine", "gale", "reno", "evil", "horn", "circ",
            "wynn", "pleb", "sura", "alta", "peen", "bilk", "dunt", "kook", "raze", "luca"
        };

        word5 = new string[]
        {
            "huffy", "tonic", "folio", "dolph", "stool", "crony", "humor", "lords", "yodel", "gales",
            "faker", "alike", "bites", "kazan", "kirov", "sayer", "amber", "footy", "ludie", "rater",
            "hofer", "samia", "libri", "wigan", "sense", "klong", "swiss", "adnah", "doorn", "iyyar",
            "eager", "metol", "drank", "worth", "miser", "paget", "kyoga", "desde", "bayle", "gamma",
            "inure", "hades", "nomad", "ethyl", "biddy", "ether", "lumpy", "payne", "kyros", "dozer"
        };

        word6 = new string[]
        {
            "employ", "shacko", "canula", "lesbos", "menial", "trashy", "banjul", "shrove", "scutch", "orange",
            "swiple", "gothic", "strong", "amatol", "kermes", "somnus", "sennar", "vilely", "ghibli", "orchil",
            "bailey", "maungy", "ginned", "flocci", "vernal", "immune", "bagnio", "briton", "aveiro", "cloete",
            "piquet", "febris", "betook", "colors", "coleus", "kendra", "aimful", "jansen", "tipple", "leaker",
            "couple", "diesel", "unhoed", "eduard", "teazle", "blunge", "oxygen", "prearm", "fowler", "tither"
        };

        word7 = new string[]
        {
            "kristin", "testify", "moseley", "pompano", "kilobar", "whining", "alcaide", "cutover", "heizing", "myocoel",
            "incluse", "regroup", "georama", "ephesus", "felsite", "analogy", "locoman", "unopted", "outpush", "indoors",
            "slavkov", "strozza", "penaria", "airplot", "sheriff", "amyelia", "unlocal", "smoothy", "castoff", "cockily",
            "unicorn", "behoofs", "maureen", "coronis", "leporid", "tolbert", "peptise", "effulge", "retiree", "untrite",
            "absinth", "whangee", "ciliate", "herbert", "subduct", "unsling", "shingle", "lampion", "unmeted", "lockbox"
        };

    }

}
