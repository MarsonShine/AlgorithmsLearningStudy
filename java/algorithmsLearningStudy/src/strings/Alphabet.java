import java.util.ArrayList;

/**
 * 字母表
 */
public class Alphabet {
    private char[] chars;
    private ArrayList<Character> indices; // 可以用链表
    private int size;

    public Alphabet(String s) {
        chars = s.toCharArray();
        size = s.length();
        indices = new ArrayList<>();
        for (int i = 0; i < chars.length; i++) {
            indices.add(chars[i]);
        }
    }

    /**
     * 获取字母表中索引未知的字符
     * 
     * @param index
     * @return
     */
    public char toChar(int index) {
        return chars[index];
    }

    /**
     * 获取字符在字母表中的索引
     * 
     * @param c
     * @return
     */
    public int toIndex(char c) {
        return indices.indexOf(c);
    }

    /**
     * c是否在字母表中
     * 
     * @param c
     * @return
     */
    public boolean contains(char c) {
        for (int i = 0; i < chars.length; i++) {
            if (chars[i] == c)
                return true;
        }
        return false;
    }

    /**
     * 基数（字母表中的字符数量）
     * 
     * @return
     */
    public int R() {
        return size;
    }

    /**
     * 表示一个索引所需的比特数
     * 
     * @return
     */
    public int lgR() {
        return (int)(Math.log(size)/Math.log(2));
    }

    /**
     * 将s转换为R进制的整数
     * 
     * @param s
     * @return
     */
    public int[] toIndices(String s) {
        int[] indices = new int[s.length()];
        char[] cs = s.toCharArray();
        for (int i = 0; i < cs.length; i++) {
            indices[i] = toIndex(cs[i]);
        }
        return indices;
    }

    /**
     * 将R进制整数转换为字符串
     * 
     * @param indices
     * @return
     */
    public String toChars(int[] indices) {
        char[] cs = new char[indices.length];
        for (int i = 0; i < indices.length; i++) {
            cs[i] = toChar(indices[i]);
        }
        return new String(cs);
    }
}
