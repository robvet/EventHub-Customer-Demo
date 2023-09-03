# EventHub-Demo

Use Case:
An Azure EventHub demo app that involves publishes multiple transaction types (gasoline sales, grocery sales, and lottery ticket sales) across separate partitions. Although specifying a transaction type to a specific partition can impact the overall distribution of events across an EventHub, i.e., uneven numbers of events in each partition, it does preserve order, which this example demonstrates.

Keep in mind this demo is in a (very) early stage with much more funtionality and fine-tuning to be added.
