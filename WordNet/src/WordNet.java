/**
 * Created by rurum on 1/17/2017.
 */
import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.ST;

public class WordNet {
    private ST<String, Integer> nouns;  // string -> index


    // constructor takes the name of the two input files
    public WordNet(String synsets, String hypernyms){
        nouns = new ST<>();
        In in = new In(synsets);
        while(!in.isEmpty()){
            String[] synset = in.readLine().split(",");
            Integer synsetId = Integer.parseInt(synset[0]);
            String[] synsetNouns = synset[1].split(" ");
            for (String noun: synsetNouns) {
                nouns.put(noun, synsetId);
            }
        }
    }

    // returns all WordNet nouns
    public Iterable<String> nouns(){
        return nouns.keys();
    }

    // is the word a WordNet noun?
    public boolean isNoun(String word){
        return nouns.contains(word);
    }

    // distance between nounA and nounB (defined below)
    public int distance(String nounA, String nounB){

    }

    // a synset (second field of synsets.txt) that is the common ancestor of nounA and nounB
    // in a shortest ancestral path (defined below)
    public String sap(String nounA, String nounB){

    }

    // do unit testing of this class
    public static void main(String[] args){

    }
}
