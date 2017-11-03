/**
 * Created by rurum on 1/17/2017.
 */
import edu.princeton.cs.algs4.Digraph;
import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.Queue;
import edu.princeton.cs.algs4.ST;

public class WordNet {
    private ST<String, Integer> nouns;  // string -> index
    private SAP sap;

    // constructor takes the name of the two input files
    public WordNet(String synsets, String hypernyms){

        if (synsets == null || hypernyms == null) {
            throw new NullPointerException();
        }

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

        //build graph
        Digraph graph = new Digraph(nouns.size());
        in = new In(hypernyms);
        while(!in.isEmpty()){
            String[] hypernym = in.readLine().split(",");
            int v = Integer.parseInt(hypernym[0]);
            for (int i = 1; i < hypernym.length; i++){
                int w = Integer.parseInt(hypernym[i]);
                graph.addEdge(v, w);
            }
        }

        this.sap = new SAP(graph);
    }

    // returns all WordNet nouns
    public Iterable<String> nouns(){
        return nouns.keys();
    }

    // is the word a WordNet noun?
    public boolean isNoun(String word){
        if (word == null){
            throw new NullPointerException();
        }

        return nouns.contains(word);
    }

    // distance between nounA and nounB (defined below)
    public int distance(String nounA, String nounB){
        if (nounA == null || nounB == null) {
            throw new NullPointerException();
        }

        int nounAId = nouns.get(nounA);
        int nounBId = nouns.get(nounB);
        if (nounAId == nounBId){
            return 0;
        }

        boolean[] aMarked = new boolean[graph.V()];
        aMarked[nounAId] = true;
        int[] aDistTo = new int[graph.V()];
        aDistTo[nounAId] = 0;
        Queue<Integer> queue = new Queue<>();
        queue.enqueue(nounAId);
        while(!queue.isEmpty()){
            int v = queue.dequeue();
            for (int w: graph.adj(v)){
                if (!aMarked[w]){
                    aDistTo[w] = aDistTo[v] + 1;
                    aMarked[w] = true;
                    queue.enqueue(w);
                }
            }
        }

        boolean[] bMarked = new boolean[graph.V()];
        aMarked[nounBId] = true;
        int[] bDistTo = new int[graph.V()];
        bDistTo[nounBId] = 0;
        queue = new Queue<>();
        queue.enqueue(nounBId);
        while(!queue.isEmpty()){
            int v = queue.dequeue();

            if (aMarked[v]){
                return aDistTo[v] + bDistTo[v];
            }

            for (int w : graph.adj(v)) {
                if (!bMarked[w]){
                    bDistTo[w] = bDistTo[v] + 1;
                    bMarked[w] = true;
                    queue.enqueue(w);
                }
            }
        }

        //this should not be executed never
        return 0;
    }

    // a synset (second field of synsets.txt) that is the common ancestor of nounA and nounB
    // in a shortest ancestral path (defined below)
    public String sap(String nounA, String nounB){
        if (nounA == null || nounB == null) {
            throw new NullPointerException();
        }
    }

    // do unit testing of this class
    public static void main(String[] args){

    }
}
